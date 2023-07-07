using Utilities.Enums;

namespace Access.Contract.Roles
{
    public record CreateRoleAccessRequest(
        string Name,
        string Code,
        IEnumerable<PermissionName> Permissions
    );
}
