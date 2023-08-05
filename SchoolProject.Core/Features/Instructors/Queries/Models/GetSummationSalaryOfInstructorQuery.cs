using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Instructors.Queries.Models
{
    public class GetSummationSalaryOfInstructorQuery : IRequest<Response<decimal>>
    {

    }
}
