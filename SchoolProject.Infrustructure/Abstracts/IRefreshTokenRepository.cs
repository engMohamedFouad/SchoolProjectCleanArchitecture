using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustructure.InfrastructureBases;

namespace SchoolProject.Infrustructure.Abstracts
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {

    }
}
