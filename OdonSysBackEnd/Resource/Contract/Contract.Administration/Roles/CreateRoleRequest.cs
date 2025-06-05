using Utilities.Enums;

namespace Contract.Administration.Roles;

public record CreateRoleRequest
(
    string Name,
    string Code,
    IEnumerable<PermissionName> Permissions
);
