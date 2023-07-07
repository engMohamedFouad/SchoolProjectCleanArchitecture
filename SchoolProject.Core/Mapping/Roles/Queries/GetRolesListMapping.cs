using SchoolProject.Core.Features.Authorization.Quaries.Results;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.Roles
{
    public partial class RoleProfile
    {
        public void GetRolesListMapping()
        {
            CreateMap<Role, GetRolesListResult>();
        }
    }
}
