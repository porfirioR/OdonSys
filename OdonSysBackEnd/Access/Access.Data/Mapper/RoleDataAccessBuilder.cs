using Access.Contract.Roles;
using Access.Contract.Users;
using Access.Sql.Entities;
using Utilities.Extensions;

namespace Access.Data.Mapper
{
    internal class RoleDataAccessBuilder : IRoleDataAccessBuilder
    {
        public Role MapCreateRoleAccessRequestToRole(CreateRoleAccessRequest createRoleAccessRequest)
        {
            var role = new Role()
            {
                Active = true,
                Code = createRoleAccessRequest.Code,
                Name = createRoleAccessRequest.Name,
                RolePermissions = createRoleAccessRequest.Permissions.Select(x => new Permission { Id = Guid.NewGuid(), Name = x, Active = true })
            };
            return role;
        }

        public RoleAccessModel MapRoleToRoleAccessModel(Role role)
        {
            var roleAccessModel = new RoleAccessModel(
                role.Name,
                role.Code,
                role.RolePermissions.Select(x => x.Name.GetDescription()),
                new List<DoctorDataAccessModel>(),
                role.DateCreated,
                role.DateModified,
                role.UserCreated,
                role.UserUpdated
            );
            return roleAccessModel;
        }

        public Role MapUpdateRoleAccessRequestToRole(UpdateRoleAccessRequest updateRoleAccessRequest)
        {
            var role = new Role()
            {
                Name = updateRoleAccessRequest.Name,
                Code = updateRoleAccessRequest.Code,
            };
            return role;
        }

        public DoctorDataAccessModel MapUserRoleToDoctorDataAccessModel(UserRole role)
        {
            throw new NotImplementedException();
        }

        public Role MapUpdateRoleAccessRequestToRole(UpdateRoleAccessRequest updateRoleAccessRequest, Role role)
        {
            role.DateModified = DateTime.Now;
            role.RolePermissions = updateRoleAccessRequest.Permissions.Select(x => new Permission { Name = x });
            return role;
        }
    }
}
