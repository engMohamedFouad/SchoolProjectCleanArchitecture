using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Quaries.Results;

namespace SchoolProject.Core.Features.Authorization.Quaries.Models
{
    public class GetRolesListQuery : IRequest<Response<List<GetRolesListResult>>>
    {
    }
}
