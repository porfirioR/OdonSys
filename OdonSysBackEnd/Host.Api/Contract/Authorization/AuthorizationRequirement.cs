using Microsoft.AspNetCore.Authorization;
using Utilities.Enums;

namespace Host.Api.Contract.Authorization;

public class AuthorizationRequirement : IAuthorizationRequirement
{
    public IEnumerable<PermissionName> Permissions { get; }

    public AuthorizationRequirement(PermissionName permission) : this(new List<PermissionName> { permission }) { }

    public AuthorizationRequirement(IEnumerable<PermissionName> permissionList)
    {
        Permissions = permissionList;
    }
}
