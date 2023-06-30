using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.DTOs;

namespace SchoolProject.Core.Features.Authorization.Commands.Models
{
    public class EditRoleCommand : EditRoleRequest, IRequest<Response<string>>
    {

    }
}
