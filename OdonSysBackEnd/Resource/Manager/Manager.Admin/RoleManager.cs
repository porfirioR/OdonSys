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

        public Task<IEnumerable<RoleModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PermissionModel> GetAllPermissionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetPermissonsByRolesAsync(IEnumerable<string> roles)
        {
            throw new NotImplementedException();
        }

        public Task<RoleModel> GetRoleByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<RoleModel> UpdateAsync(UpdateRoleRequest accessRequest)
        {
            throw new NotImplementedException();
        }
    }
}
