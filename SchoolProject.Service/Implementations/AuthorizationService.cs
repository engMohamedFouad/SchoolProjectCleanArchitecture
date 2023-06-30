using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{

    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        #endregion
        #region Constructors
        public AuthorizationService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }


        #endregion
        #region handle Functions
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return "Success";
            return "Failed";
        }

        public async Task<bool> IsRoleExist(string roleName)
        {
            //var role=await _roleManager.FindByNameAsync(roleName);
            //if(role == null) return false;
            //return true;
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            //check role is exist or not
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
                return "notFound";
            role.Name= request.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return "Success";
            var errors = string.Join("-", result.Errors);
            return errors;
        }
        #endregion
    }
}
