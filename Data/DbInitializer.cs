
//using Microsoft.AspNetCore.Http.HttpResults;
using Core.Constants.Enums;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Data
{
    public static class DbInitializer
    {
        public static async Task SeedDataAsync(RoleManager<IdentityRole> roleManager,UserManager<User>userManager) 
        {
            await AddRolesAsync(roleManager);
           await AddAdminAsync(userManager,roleManager);
        }
        private static async  Task AddRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetValues<UserRoles>())
            {
                if (!(await roleManager.RoleExistsAsync(role.ToString())))
                {
                    _=roleManager.CreateAsync(new IdentityRole
                    {
                        Name=role.ToString(),
                    }).Result;
                } 
            }
        } 
        private static async Task AddAdminAsync(UserManager<User>userManager,RoleManager<IdentityRole> roleManager)
        {
            if (await userManager.FindByEmailAsync("Admin@test.com") is null)
            {

                var user = new User
                {
                    UserName = "Admin@test.com",
                    Email = "Admin@test.com"
                };

                var result = await userManager.CreateAsync(user, "AdminT123!");
                if (!result.Succeeded) throw new Exception("cannot be created admin");

                var role = await roleManager.FindByNameAsync("Admin");
                if (role?.Name is null) throw new Exception("have not Admin role");


                var addToRoleResult = await userManager.AddToRoleAsync(user, role.Name);
                if (!addToRoleResult.Succeeded) throw new Exception("cannot be added Admin role to user");
            }


        }
    }
}
