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
using ShredMates.Services.Models;
using ShredMates.Web.Infrastructure.Extensions;
using System;
using System.Collections.Generic;

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


            // facebook auth
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "331814970630527";
                facebookOptions.AppSecret = "f9a5dd07f11fe3a64932940ed16fa946";
            });

            services.AddAutoMapper(); // auto mapping

            services.AddServices(); // auto services

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // reg http service

            services.AddSingleton(sp => new ShoppingCart() { Id = Guid.NewGuid().ToString(), ShoppingCartItems = new List<ShoppingCartItem>() });

            services.AddRouting(routing => { routing.LowercaseUrls = true; }); // routing lowercase

            services.AddMvc(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>(); // auto AntiforgeryToken
            });

            services.AddAuthorization(); // auth
            services.AddDistributedMemoryCache(); // cache
            services.AddSession(options =>  // session
            {
                options.IdleTimeout = TimeSpan.FromSeconds(30);
            });
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

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession(); // session always before app.UseMvc

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "shopping cart",
                //    template: "shoppingcart/index",
                //    defaults: new { contoller = "ShoppingCart", action = "Index" }
                //    );

                routes.MapRoute(
                name: "products",
                template: "products/{id}/{title}",
                defaults: new { controller = "Products", action = "Details" }
                );

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
