using SchoolProject.Infrustructure.InfrastructureBases;

namespace SchoolProject.Infrustructure.Abstracts.Views
{
    public interface IViewRepository<T> : IGenericRepositoryAsync<T> where T : class
    {
    }
}
