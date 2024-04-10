using GBC_Travel_Group_90.Areas.TravelManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace GBC_Travel_Group_90.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Traveler.ToString()));
        }

        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Admin User
            var admin = new ApplicationUser
            {
                UserName = "Admin",
                Email = "travel90-agency@domain.ca",
                FirstName = "Admin",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != admin.Id))
            {
                // Attempt to find the admin by their email address.
                var user = await userManager.FindByEmailAsync(admin.Email);

                // If the admin does not exist, proceed with creation.
                if (user == null)
                {
                    // Create the admin account with a specified password.
                    await userManager.CreateAsync(admin, "Password1!@");
                    // Assign the admin with all the following roles
                    await userManager.AddToRoleAsync(admin, Enum.Roles.Admin.ToString());
                }
            }
        }
    }
}
