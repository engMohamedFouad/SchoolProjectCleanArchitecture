using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.AuthServices.Interfaces;

namespace SchoolProject.Core.Filters
{
    public class AuthFilter : IAsyncActionFilter
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<User> _userManager;
        public AuthFilter(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated==true)
            {
                var roles = await _currentUserService.GetCurrentUserRolesAsync();
                if (roles.All(x => x!="User"))
                {
                    context.Result=new ObjectResult("Forbidden")
                    {
                        StatusCode = StatusCodes.Status403Forbidden
                    };
                }
                else
                {
                    await next();
                }

            }
        }
    }
}

