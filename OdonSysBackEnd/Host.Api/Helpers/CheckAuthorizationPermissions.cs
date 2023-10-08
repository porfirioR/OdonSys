using Contract.Administration.Roles;
using Host.Api.Contract.Authorization;
using Microsoft.AspNetCore.Authorization;
using Utilities;

namespace Host.Api.Helpers
{
    public class CheckAuthorizationPermissions : AuthorizationHandlerBase<CheckAuthorizationPermissions, AuthorizationRequirement>
    {
        private readonly IRoleManager _roleManager;
        public CheckAuthorizationPermissions(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        protected override async Task CheckRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirement requirement)
        {
            // Get all of a user's roles
            var userIdAadB2C = context.User.FindFirst(x => x.Type == Claims.UserIdAadB2C)?.Value;
            var user = context.User.FindFirst(x => x.Type == Claims.UserId);
            var userId = user != null ? user.Value : string.Empty;
            var permissions = await _roleManager.GetPermissionsByUserIdAsync(userId, userIdAadB2C);
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
