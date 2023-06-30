using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>
    {
        #region MyRegion
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;

        #endregion
        #region MyRegion
        public RoleCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                  IAuthorizationService authorizationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
        }

        #endregion
        #region MyRegion
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName);
            if (result=="Success") return Success("");
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request);
            if (result=="notFound") return NotFound<string>();
            else if (result=="Success") return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);
            else
                return BadRequest<string>(result);
        }
        #endregion

    }
}
