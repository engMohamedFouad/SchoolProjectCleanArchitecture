using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                  IMapper mapper,
                                  UserManager<User> userManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _userManager= userManager;
        }


        #endregion

        #region Handle Functions

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //if Email is Exist
            var user = await _userManager.FindByEmailAsync(request.Email);
            //email is Exist
            if (user!=null) return BadRequest<string>(_sharedResources[SharedResourcesKeys.EmailIsExist]);

            //if username is Exist
            var userByUserName = await _userManager.FindByNameAsync(request.UserName);
            //username is Exist
            if (userByUserName!=null) return BadRequest<string>(_sharedResources[SharedResourcesKeys.UserNameIsExist]);

            //Mapping
            var identityUser = _mapper.Map<User>(request);
            //Create
            var createResult = await _userManager.CreateAsync(identityUser, request.Password);
            //Failed
            if (!createResult.Succeeded)
                return BadRequest<string>(createResult.Errors.FirstOrDefault().Description);
            //message

            //Sucess
            return Created("");
        }
        #endregion
    }
}
