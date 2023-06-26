using Contract.Admin.Roles;
using Host.Api.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Utilities;

namespace Host.Api.Helpers
{
    public class CheckAuthPermissions : AuthorizationHandlerBase<CheckAuthPermissions, AuthRequirement>
    {
        private readonly IRoleManager _roleManager;
        public CheckAuthPermissions(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        protected override async Task CheckRequirementAsync(AuthorizationHandlerContext context, AuthRequirement requirement)
        {
            // Get all of a user's roles
            var userId = context.User.FindFirst(x => x.Type == Claims.UserId).Value;
            var permissions = await _roleManager.GetPermissionsByUserIdAsync(userId);
            var requirementPermissions = requirement.Permissions.Select(x => x.ToString()).ToList();

            // Check to see if the use has the function required
            if (permissions.Any(x => requirementPermissions.Contains(x)))
            {
                context.Succeed(requirement);
            }
            else
            {
                var failureReason = new AuthorizationFailureReason(this, $"Se requiere uno de los siguientes permisos: {string.Join(", ", requirementPermissions)}");
                context.Fail(failureReason);
            }

            await Task.CompletedTask;
        }
    }
}
