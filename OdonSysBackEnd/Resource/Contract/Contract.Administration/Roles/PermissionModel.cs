using Utilities.Enums;

namespace Contract.Administration.Roles
{
    public record PermissionModel(
        string Name,
        PermissionName Code,
        PermissionGroup Group,
        PermissionSubGroup SubGroup
    );
}
