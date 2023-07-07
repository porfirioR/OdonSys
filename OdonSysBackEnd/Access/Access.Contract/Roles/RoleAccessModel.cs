using Access.Contract.Users;

namespace Access.Contract.Roles
{
    public record RoleAccessModel(
        string Name,
        string Code,
        IEnumerable<string> RolePermissions,
        IEnumerable<DoctorDataAccessModel> UserRoles,
        DateTime DateCreated,
        DateTime DateModified,
        string UserCreated,
        string UserUpdated
    );
}
