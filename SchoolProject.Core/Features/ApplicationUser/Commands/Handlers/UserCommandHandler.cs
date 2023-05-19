namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>
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

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            //check if user is exist
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            //if Not Exist notfound
            if (oldUser==null) return NotFound<string>();
            //mapping
            var newUser = _mapper.Map(request, oldUser);
            //update
            var result = await _userManager.UpdateAsync(newUser);
            //result is not success
            if (!result.Succeeded) return BadRequest<string>(_sharedResources[SharedResourcesKeys.UpdateFailed]);
            //message
            return Success((string)_sharedResources[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //if Not Exist notfound
            if (user==null) return NotFound<string>();
            //Delete the User
            var result = await _userManager.DeleteAsync(user);
            //in case of Failure
            if (!result.Succeeded) return BadRequest<string>(_sharedResources[SharedResourcesKeys.DeletedFailed]);
            return Success((string)_sharedResources[SharedResourcesKeys.Deleted]);
        }
        #endregion
    }
}
