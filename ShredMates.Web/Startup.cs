﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShredMates.Data;
using ShredMates.Data.Models;
using ShredMates.Services.Common;
using ShredMates.Services.Models;
using ShredMates.Web.Controllers;
using ShredMates.Web.Infrastructure.Extensions;
using System;
using System.Collections.Generic;

namespace ShredMates.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services
               .Configure<CookiePolicyOptions>(options =>
               {
                   options.CheckConsentNeeded = context => true;
                   options.MinimumSameSitePolicy = SameSiteMode.None;
               });

            services.AddDbContext<ShredMatesDbContext>(options
                => options.UseSqlServer(Configuration.GetDefaultConnectionString()));

            services
                .AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ShredMatesDbContext>();

            services
               .Configure<IdentityOptions>(options =>
               {
                   options.Password.RequireDigit = false;
                   options.Password.RequiredLength = 6;
                   options.Password.RequireLowercase = false;
                   options.Password.RequireNonAlphanumeric = false;
                   options.Password.RequireUppercase = false;
               });

            // Shopping cart
            services.AddSingleton(s => new ShoppingCart()
            {
                Id = Guid.NewGuid().ToString(),
                ShoppingCartItems = new List<ShoppingCartItem>()
            });

            services.AddServices(); // auto reg all services

            // auto req all mappings
            services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(typeof(ITransientService).Assembly,
                                   typeof(HomeController).Assembly);


            services
                .AddMvc(options => options.AddAutoValidateAntiforgeryToken())
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddSessionStateTempDataProvider(); // add TempData

            services.AddRouting(routing => { routing.LowercaseUrls = true; });

            services.AddSession(options => options.IdleTimeout = TimeSpan.FromSeconds(30));

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandling(env);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession(); // always before app.UseEndpoints();!

            app.UseEndpoints();

            //app.SeedData();
        }
    }
}