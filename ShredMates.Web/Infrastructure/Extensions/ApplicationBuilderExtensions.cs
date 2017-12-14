using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShredMates.Data;
using ShredMates.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShredMates.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ShredMatesDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();


                // create roles
                Task.Run(async () =>
                {
                    var roles = new[]
                    {
                            WebConstants.AdminRole,
                            WebConstants.ModeratorRole
                    };

                    foreach (var role in roles)
                    {
                        var roleExists = await roleManager.RoleExistsAsync(role);

                        if (!roleExists)
                        {
                            var result = await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = role
                            });

                            var error = result.Errors.SelectMany(e => e.Code);               
                        }
                    }

                    // create admin
                    var admin = await userManager.FindByNameAsync("admin@test.com");

                    if (admin == null)
                    {
                        admin = new User
                        {
                            UserName = "admin@test.com",
                            Email = "admin@test.com"
                        };

                        await userManager.CreateAsync(admin, "admin12");

                        await userManager.AddToRoleAsync(admin, WebConstants.AdminRole);
                    }
                })
                .Wait();
            }

            return app;
        }
    }
}
