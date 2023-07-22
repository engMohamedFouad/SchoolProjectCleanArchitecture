using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Queries.Handles
{
    public class AuthenticationQueryHandler : ResponseHandler,
        IRequestHandler<AuthorizeUserQuery, Response<string>>,
        IRequestHandler<ConfirmEmailQuery, Response<string>>,
        IRequestHandler<ConfirmResetPasswordQuery, Response<string>>
    {


        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region Constructors
        public AuthenticationQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                            IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer=stringLocalizer;
            _authenticationService=authenticationService;
        }


        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            if (result=="NotExpired")
                return Success(result);
            return Unauthorized<string>(_stringLocalizer[SharedResourcesKeys.TokenIsExpired]);
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var confirmEmail = await _authenticationService.ConfirmEmail(request.UserId, request.Code);
            if (confirmEmail=="ErrorWhenConfirmEmail")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.ErrorWhenConfirmEmail]);
            return Success<string>(_stringLocalizer[SharedResourcesKeys.ConfirmEmailDone]);
        }

        public async Task<Response<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ConfirmResetPassword(request.Code, request.Email);
            switch (result)
            {
                case "UserNotFound": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.InvaildCode]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.InvaildCode]);
            }
        }
        #endregion
    }
}

