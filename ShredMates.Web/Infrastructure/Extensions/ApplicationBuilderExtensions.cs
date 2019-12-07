using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShredMates.Data;
using ShredMates.Data.Models;
using System;
using System.Threading.Tasks;

namespace ShredMates.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            return app;
        }
        public static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
               => app.UseEndpoints(routes =>
          {
              routes.MapControllerRoute(
                 name: "products",
                 pattern: "products/{id}/{title}",
                 defaults: new { controller = "Products", action = "Details" }
                 );
              routes.MapRazorPages();

              routes.MapControllerRoute(
                         name: "category",
                         pattern: "products/{id}/{title}",
                         defaults: new { controller = "Category", action = "Index" }
                         );
              routes.MapRazorPages();

              routes.MapControllerRoute(
                 name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
              routes.MapRazorPages();

              routes.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
              routes.MapRazorPages();
          });

        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
            {
                IServiceProvider services = serviceScope.ServiceProvider;

                ShredMatesDbContext db = services.GetService<ShredMatesDbContext>();

                await db.Database.MigrateAsync();

                var roleManager = services.GetService<RoleManager<IdentityRole>>();

                // create role = "Administrator"
                if (!await roleManager.RoleExistsAsync(WebConstants.AdminRole))
                {
                    IdentityRole adminRole = new IdentityRole(WebConstants.AdminRole);
                    await roleManager.CreateAsync(adminRole);
                }
                //create role = "Moderator"
                if (!await roleManager.RoleExistsAsync(WebConstants.AdminRole))
                {
                    IdentityRole moderatorRole = new IdentityRole(WebConstants.ModeratorRole);
                    await roleManager.CreateAsync(moderatorRole);
                }

                var userManager = services.GetService<UserManager<User>>();

                // create admin for testing
                if (await userManager.FindByNameAsync("admin@ShredMates.com") == null)
                {
                    User admin = new User
                    {
                        UserName = "admin@ShredMates.com",
                        Email = "admin@ShredMates.com"
                    };

                    await userManager.CreateAsync(admin, "adminpass");
                    await userManager.AddToRoleAsync(admin, WebConstants.AdminRole);
                    await userManager.AddToRoleAsync(admin, WebConstants.ModeratorRole);
                }

                // create user for testing
                if (await userManager.FindByNameAsync("user@ShredMates.com") == null)
                {
                    User user = new User
                    {
                        UserName = "user@ShredMates.com",
                        Email = "user@ShredMates.com"
                    };
                    await userManager.CreateAsync(user, "userpass");
                    await userManager.AddToRoleAsync(user, WebConstants.ModeratorRole);
                }

                // create category for testing
                var snowboard = new Category
                {
                    Name = "Snowboard"
                };
                if (await db.Categories.FirstOrDefaultAsync(
                    c => c.Name == snowboard.Name) == null)
                {
                    await db.Categories.AddAsync(snowboard);
                    await db.SaveChangesAsync();
                }

                // create product for testing
                if (await db.Products.CountAsync() == 0)
                {
                    var testProduct = new Product
                    {
                        CategoryId = snowboard.Id,
                        Title = "Sensei"
                    };

                    var testProduct2 = new Product
                    {
                        CategoryId = snowboard.Id,
                        Title = "Slash"
                    };

                    var testProduct3 = new Product
                    {
                        CategoryId = snowboard.Id,
                        Title = "Luna"
                    };

                    await db.Products.AddRangeAsync(testProduct, testProduct2, testProduct3);
                }

                await db.SaveChangesAsync();
            }

            return app;
        }
        public static IApplicationBuilder SeedData(this IApplicationBuilder app) => app.SeedDataAsync().GetAwaiter().GetResult();
    }
}
