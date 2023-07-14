using Access.Contract.Roles;

namespace Contract.Administration.Roles
{
    public interface IRoleManagerBuilder
    {
        CreateRoleAccessRequest MapCreateRoleRequestToCreateRoleAccessRequest(CreateRoleRequest createRoleRequest);
        UpdateRoleAccessRequest MapUpdateRoleRequestToUpdateRoleAccessRequest(UpdateRoleRequest updateRoleRequest);
        RoleModel MapRoleAccessModelToRoleModel(RoleAccessModel roleAccessModel);
    }
}
