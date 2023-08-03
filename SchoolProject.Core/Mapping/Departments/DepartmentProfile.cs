using AutoMapper;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            GetDepartmentByIdMapping();
            GetDepartmentStudentCountMapping();
            GetDepartmentStudentCountByIdMapping();
        }
    }
}
