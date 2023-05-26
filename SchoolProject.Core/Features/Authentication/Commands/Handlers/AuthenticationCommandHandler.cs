using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<string>>
    {


        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region Constructors
        public AuthenticationCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                            UserManager<User> userManager,
                                            SignInManager<User> signInManager,
                                            IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer=stringLocalizer;
            _userManager=userManager;
            _signInManager=signInManager;
            _authenticationService=authenticationService;
        }


        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if user is exist or not
            var user = await _userManager.FindByNameAsync(request.UserName);
            //Return The UserName Not Found
            if (user==null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameIsNotExist]);
            //try To Sign in 
            var signInResult = _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            //if Failed Return Passord is wrong
            if (!signInResult.IsCompletedSuccessfully) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]);

            //Generate Token
            var accessToken = await _authenticationService.GetJWTToken(user);
            //return Token 
            return Success(accessToken);
        }

        #endregion
    }
}
