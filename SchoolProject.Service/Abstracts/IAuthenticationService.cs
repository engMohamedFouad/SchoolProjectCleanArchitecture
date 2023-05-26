using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<string> GetJWTToken(User user);
    }
}
