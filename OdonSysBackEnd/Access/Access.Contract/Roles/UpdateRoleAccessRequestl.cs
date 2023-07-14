using Utilities.Enums;

namespace Access.Contract.Roles
{
    public record UpdateRoleAccessRequest
    (
        string Name,
        string Code,
        bool Active,
        IEnumerable<PermissionName> Permissions
    );
}
