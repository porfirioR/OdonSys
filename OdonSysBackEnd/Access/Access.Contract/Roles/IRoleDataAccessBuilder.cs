using Access.Contract.Users;
using Access.Sql.Entities;
using Utilities.Enums;

namespace Access.Contract.Roles
{
    public interface IRoleDataAccessBuilder
    {
        Role MapCreateRoleAccessRequestToRole(CreateRoleAccessRequest createRoleAccessRequest);
        RoleAccessModel MapRoleToRoleAccessModel(Role role, IEnumerable<Permission> rolePeremissions = null);
        DoctorDataAccessModel MapUserRoleToDoctorDataAccessModel(UserRole role);
        Role MapUpdateRoleAccessRequestToRole(UpdateRoleAccessRequest updateRoleAccessRequest);
        Role MapUpdateRoleAccessRequestToRole(UpdateRoleAccessRequest updateRoleAccessRequest, Role role);
        IEnumerable<Permission> GetPermissions(IEnumerable<PermissionName> permissions, Role role);
    }
}
