
using Agro.Model.Data;
using Agro.Model.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Agro.Initializer
{
    public static class DbInitializer
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                var _userManager =
                         serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var _roleManager =
                         serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                if (!context.Users.Any(usr => usr.UserName == "admin@ad.me"))
                {
                    var user = new ApplicationUser()
                    {
                        UserName = "admin@ad.me",
                        Email = "admin@ad.me",
                        EmailConfirmed = true,
                    };

                    var userResult = _userManager.CreateAsync(user, "P@ssw0rd").Result;
                }

                if (!_roleManager.RoleExistsAsync("Admin").Result)
                {
                    var role = _roleManager.CreateAsync
                               (new IdentityRole { Name = "Admin" }).Result;
                }

                if (!_roleManager.RoleExistsAsync("Moderator").Result)
                {
                    var role = _roleManager.CreateAsync
                               (new IdentityRole { Name = "Moderator" }).Result;
                }
                
                if (!_roleManager.RoleExistsAsync("User").Result)
                {
                    var role = _roleManager.CreateAsync
                               (new IdentityRole { Name = "User" }).Result;
                }

                //var adminUser = _userManager.FindByNameAsync("admin@ad.me").Result;
                //var userRole = _userManager.AddToRolesAsync
                //               (adminUser, new string[] { "Admin" }).Result;


                context.SaveChanges();
            }
        }
    }
}
