using Access.Contract.Users;
using Access.Sql.Entities;

namespace Access.Contract.Roles
{
    public interface IRoleDataAccessBuilder
    {
        Role MapCreateRoleAccessRequestToRole(CreateRoleAccessRequest createRoleAccessRequest);
        RoleAccessModel MapRoleToRoleAccessModel(Role role);
        DoctorDataAccessModel MapUserRoleToDoctorDataAccessModel(UserRole role);
        Role MapUpdateRoleAccessRequestToRole(UpdateRoleAccessRequest updateRoleAccessRequest);
        Role MapUpdateRoleAccessRequestToRole(UpdateRoleAccessRequest updateRoleAccessRequest, Role role);
    }
}
