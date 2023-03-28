using Utilities.Enums;

namespace Contract.Admin.Roles
{
    public record PermissionModel(
        string Name,
        PermissionName Code,
        PermissionGroup Group,
        PermissionSubGroup SubGroup
    ) { }
}
