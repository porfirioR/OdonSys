using Microsoft.AspNetCore.Authorization;
using Utilities.Enums;

namespace Host.Api.Models.Auth
{
    public class AuthRequirement : IAuthorizationRequirement
    {
        public IEnumerable<PermissionName> Permissions { get; }

        public AuthRequirement(PermissionName permission) : this(new List<PermissionName> { permission }) { }

        public AuthRequirement(IEnumerable<PermissionName> permissionList)
        {
            Permissions = permissionList;
        }
    }
}
