using Contract.Administration.Users;

namespace Contract.Administration.Roles
{
    public record RoleModel
    (
        string Name,
        string Code,
        DateTime DateCreated,
        DateTime DateModified,
        string UserCreated,
        string UserUpdated,
        IEnumerable<string> RolePermissions,
        IEnumerable<DoctorModel> UserRoles
    );
}
