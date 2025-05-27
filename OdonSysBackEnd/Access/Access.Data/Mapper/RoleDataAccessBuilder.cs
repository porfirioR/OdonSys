using Access.Contract.Roles;
using Access.Contract.Users;
using Access.Sql.Entities;
using Utilities.Enums;
using Utilities.Extensions;

namespace Access.Data.Mapper;

internal sealed class RoleDataAccessBuilder : IRoleDataAccessBuilder
{
    public Role MapCreateRoleAccessRequestToRole(CreateRoleAccessRequest createRoleAccessRequest)
    {
        var role = new Role()
        {
            Active = true,
            Code = createRoleAccessRequest.Code,
            Name = createRoleAccessRequest.Name
        };
        return role;
    }

    public IEnumerable<Permission> GetPermissions(IEnumerable<PermissionName> permissions, Role role)
    {
        return permissions.Select(x => new Permission { Id = Guid.NewGuid(), Name = x, Active = true, Role = role });
    }

    public RoleAccessModel MapRoleToRoleAccessModel(Role role, IEnumerable<Permission> rolePermissions = null)
    {
        var rolePermissionList = rolePermissions != null && rolePermissions.Any() ? rolePermissions.Select(x => x.Name.GetDescription()) : null;
        rolePermissionList ??= role.RolePermissions != null && role.RolePermissions.Any() ? role.RolePermissions.Select(x => x.Name.GetDescription()) : new List<string>();
        var roleAccessModel = new RoleAccessModel(
            role.Name,
            role.Code,
            rolePermissionList,
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
        role.RolePermissions = updateRoleAccessRequest.Permissions != null ? updateRoleAccessRequest.Permissions.Select(x => new Permission { Name = x }) : new List<Permission>();
        return role;
    }
}
