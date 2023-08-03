using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Results;

namespace SchoolProject.Core.Features.Department.Queries.Models
{
    public class GetDepartmentStudentCountByIDQuery : IRequest<Response<GetDepartmentStudentCountByIDResult>>
    {
        public int DID { get; set; }
    }
}
