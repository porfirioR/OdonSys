using Utilities.Enums;

namespace Contract.Administration.Roles
{
    public record UpdateRoleRequest
    (
        string Name,
        string Code,
        bool Active,
        IEnumerable<PermissionName> Permissions
    );
}
