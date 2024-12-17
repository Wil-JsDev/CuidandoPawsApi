using CuidandoPawsApi.Domain.Enum;
using CuidandoPawsApi.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Infrastructure.Identity.Seeds
{
    public static class DefaultAdmin
    {

        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            User adminUser = new User () {
                FirstName = "Jose",
                LastName = "Developer" ,
                Email = "joseSuperAdmin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "809-000-1111",
                UserName = "JDeveloper" ,
                PhoneNumberConfirmed = true,
                CreateAt = DateTime.UtcNow,
            };

            if (userManager.Users.All(u => u.Id != adminUser.Id))
            {
                var admin = await userManager.FindByEmailAsync (adminUser.Email);

                if (admin == null)
                {
                    await userManager.CreateAsync (adminUser, "1234J$se");
                    await userManager.AddToRoleAsync(adminUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(adminUser, Roles.PetOwner.ToString());
                    await userManager.AddToRoleAsync(adminUser, Roles.Caregiver.ToString());
                }
            }
        }
    }
}
