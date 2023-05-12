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
            var roles = context.User.FindAll(x => x.Type == Claims.UserRoles).Select(x => x.Value).ToList();
            var permissions = await _roleManager.GetPermissonsByRolesAsync(roles);
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
