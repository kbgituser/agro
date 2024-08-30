using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Agro.Model.Data;
using Agro.Model.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Agro.Model.DataSeed
{
    public class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServiceProviderscope = scope.ServiceProvider;
                var context = scopedServiceProviderscope.GetRequiredService<ApplicationDbContext>();

                string[] roles = new string[] { "Owner", "Administrator", "Manager", "Editor", "Buyer", "Business", "Seller", "Subscriber" , "User"};

                foreach (string roleNames in roles)
                {
                    var roleStore = new RoleStore<IdentityRole>(context);

                    if (!context.Roles.Any(r => r.Name == roleNames))
                    {
                        var role = new IdentityRole(roleNames);
                        role.NormalizedName = roleNames.ToUpper();
                        roleStore.CreateAsync(role).Wait();
                    }
                }

                var user = new ApplicationUser
                {
                    Name = "admin@ad.me",
                    Email = "admin@ad.me",
                    NormalizedEmail = "admin@ad.me",
                    UserName = "admin@ad.me",
                    NormalizedUserName = "admin@ad.me",
                    PhoneNumber = "+111111111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };


                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(user, "secret");
                    user.PasswordHash = hashed;

                    var userStore = new UserStore<ApplicationUser>(context);
                    var result = userStore.CreateAsync(user);

                }

                AssignRolesAsync(scopedServiceProviderscope, user.Email, roles).Wait();

                context.SaveChangesAsync();
            }
        }

        public static async Task<IdentityResult> AssignRolesAsync(IServiceProvider services, string email, string[] roles)
        {
            UserManager<ApplicationUser> _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);
            return result;
        }
    }
}