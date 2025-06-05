using Contract.Administration.Roles;
using Contract.Administration.Users;
using Host.Api.Contract.Authorization;
using Microsoft.AspNetCore.Authorization;
using Utilities;

namespace Host.Api.Helpers;

public class CheckAuthorizationPermissions : AuthorizationHandlerBase<CheckAuthorizationPermissions, AuthorizationRequirement>
{
    private readonly IRoleManager _roleManager;
    private readonly IUserManager _userManager;

    public CheckAuthorizationPermissions(IRoleManager roleManager, IUserManager userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    protected override async Task CheckRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirement requirement)
    {
        // Get all of a user's roles
        var userIdAadB2C = context.User.FindFirst(x => x.Type == Claims.UserIdAadB2C)?.Value;
        var user = context.User.FindFirst(x => x.Type == Claims.UserId);
        var userId = user != null ? user.Value : await _userManager.GetInternalUserIdByExternalUserIdAsync(userIdAadB2C);
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
