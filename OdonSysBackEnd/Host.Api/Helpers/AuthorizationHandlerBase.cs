using Host.Api.Contract.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Host.Api.Helpers
{
    public abstract class AuthorizationHandlerBase<THandler, TRequirement> : AuthorizationHandler<TRequirement> where TRequirement : IAuthorizationRequirement
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected AuthorizationHandlerBase() { }

        protected AuthorizationHandlerBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected abstract Task CheckRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement);
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            if (!context.IsAuthenticated())
            {
                await Task.CompletedTask;
                return;
            }

            // TODO: Check if this is valid
            if (context.HasFailed)
            {
                await Task.CompletedTask;
                return;
            }

            await CheckRequirementAsync(context, requirement);
        }
    }
}
