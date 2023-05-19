using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile
    {
        public void AddUserMapping()
        {
            CreateMap<AddUserCommand, User>();
        }
    }
}