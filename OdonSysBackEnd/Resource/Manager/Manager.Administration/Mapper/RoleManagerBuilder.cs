using Access.Contract.Roles;
using Contract.Administration.Roles;
using Contract.Administration.Users;

namespace Manager.Administration.Mapper
{
    internal class RoleManagerBuilder : IRoleManagerBuilder
    {
        public CreateRoleAccessRequest MapCreateRoleRequestToCreateRoleAccessRequest(CreateRoleRequest createRoleRequest) => new(
            createRoleRequest.Name,
            createRoleRequest.Code,
            createRoleRequest.Permissions
        );

        public RoleModel MapRoleAccessModelToRoleModel(RoleAccessModel roleAccessModel)
        {
            var userRoles = roleAccessModel.UserRoles != null && roleAccessModel.UserRoles.Any() ?
                roleAccessModel.UserRoles.Select(x => new DoctorModel(
                    x.Id,
                    x.Name,
                    x.MiddleName,
                    x.Surname,
                    x.SecondSurname,
                    x.Document,
                    x.Country,
                    x.Email,
                    x.Phone,
                    x.UserName,
                    x.Active,
                    x.Approved,
                    x.Roles
                )) :
                new List<DoctorModel>();
            var roleModel = new RoleModel(
                roleAccessModel.Name,
                roleAccessModel.Code,
                roleAccessModel.DateCreated,
                roleAccessModel.DateModified,
                roleAccessModel.UserCreated,
                roleAccessModel.UserUpdated,
                roleAccessModel.RolePermissions,
                userRoles
            );
            return roleModel;
        }

        public UpdateRoleAccessRequest MapUpdateRoleRequestToUpdateRoleAccessRequest(UpdateRoleRequest updateRoleRequest) => new(
            updateRoleRequest.Name,
            updateRoleRequest.Code,
            updateRoleRequest.Active,
            updateRoleRequest.Permissions
        );
    }
}
