using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;


namespace SchoolProject.Infrustructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> _roleManager)
        {
            var rolesCount = await _roleManager.Roles.CountAsync();
            if (rolesCount<=0)
            {

                await _roleManager.CreateAsync(new Role()
                {
                    Name="Admin"
                });
                await _roleManager.CreateAsync(new Role()
                {
                    Name="User"
                });
            }
        }

    }
}
