using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.Admin.Roles
{
    public interface IRoleManager
    {
        Task<IEnumerable<string>> GetPermissonsByRoles(IEnumerable<string> roles);
        Task<PermissionModel> GetAllPermission();
        Task<RoleModel> CreateAsync(CreateRoleRequest accessRequest);
        Task<RoleModel> UpdateAsync(UpdateRoleRequest accessRequest);
        Task<RoleModel> GetRoleByCodeAccessAsync(string code);
        Task<IEnumerable<RoleModel>> GetAllAccessAsync();
    }
}
