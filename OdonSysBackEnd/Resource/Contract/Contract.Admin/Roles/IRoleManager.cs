﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.Admin.Roles
{
    public interface IRoleManager
    {
        Task<IEnumerable<string>> GetPermissonsByRolesAsync(IEnumerable<string> roles);
        Task<IEnumerable<PermissionModel>> GetAllPermissionsAsync();
        Task<RoleModel> CreateAsync(CreateRoleRequest accessRequest);
        Task<RoleModel> UpdateAsync(UpdateRoleRequest accessRequest);
        Task<RoleModel> GetRoleByCodeAsync(string code);
        Task<IEnumerable<RoleModel>> GetAllAsync();
    }
}
