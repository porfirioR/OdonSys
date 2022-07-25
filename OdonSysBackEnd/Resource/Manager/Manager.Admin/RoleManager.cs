using Contract.Admin.Roles;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Admin
{
    internal class RoleManager : IRoleManager
    {
        public Task<RoleModel> CreateAsync(CreateRoleRequest accessRequest)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoleModel>> GetAllAccessAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PermissionModel> GetAllPermission()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetPermissonsByRoles(IEnumerable<string> roles)
        {
            throw new NotImplementedException();
        }

        public Task<RoleModel> GetRoleByCodeAccessAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<RoleModel> UpdateAsync(UpdateRoleRequest accessRequest)
        {
            throw new NotImplementedException();
        }
    }
}
