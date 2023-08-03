using SchoolProject.Data.Entities.Procedures;

namespace SchoolProject.Infrustructure.Abstracts.Procedures
{
    public interface IDepartmentStudentCountProcRepository
    {
        public Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcs(DepartmentStudentCountProcParameters parameters);
    }
}
