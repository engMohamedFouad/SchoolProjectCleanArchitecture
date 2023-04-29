using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.InfrastructureBases;

namespace SchoolProject.Infrustructure.Abstracts
{
    public interface IStudentRepository : IGenericRepositoryAsync<Student>
    {
        public Task<List<Student>> GetStudentsListAsync();
    }
}
