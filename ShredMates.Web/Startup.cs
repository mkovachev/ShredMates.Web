using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Implementations;
using ShredMates.Web.Infrastructure.Extensions;
using System;

namespace ShredMates.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShredMatesDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ShredMatesDbConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                // set password requirements
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ShredMatesDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(); // auto mapping
            services.AddServices(); // auto add services

            //services.AddHttpContextAccessor(); // for Core 2.1
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // reg http service
            services.AddScoped(session => ShoppingCartService.GetCart(session));
            // services.AddRouting(routing => { routing.LowercaseUrls = true; }); // add routing

            services.AddMvc(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>(); // auto AntiforgeryToken
            });
            //.AddSessionStateTempDataProvider(); // add TempData provider

            services.AddAuthorization();
            services.AddDistributedMemoryCache(); // add cache
            services.AddSession(); // add session

            // add session
            //services.AddSession(options =>
            //{
            //    options.Cookie.HttpOnly = false; // false - cookie is accessible through JavaScript
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // transmitted via HTTPS only
            //    options.Cookie.Name = "MySession"; // override the default cookie name
            //    options.IdleTimeout = TimeSpan.FromSeconds(30); // session expiraton in minutes
            //});
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDatabaseMigration(); // auto migrations

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "text/plain";
                await context.HttpContext.Response.WriteAsync(
                    "Status code page, status code: " +
                    context.HttpContext.Response.StatusCode);
            });

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession(); // add session always before app.UseMvc

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
