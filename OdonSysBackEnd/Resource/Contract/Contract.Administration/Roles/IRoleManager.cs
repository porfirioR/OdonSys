namespace Contract.Administration.Roles;

public interface IRoleManager
{
    Task<IEnumerable<string>> GetPermissionsByUserIdAsync(string userId);
    IEnumerable<PermissionModel> GetAllPermissions();
    Task<RoleModel> CreateAsync(CreateRoleRequest request);
    Task<RoleModel> UpdateAsync(UpdateRoleRequest request);
    Task<RoleModel> GetRoleByCodeAsync(string code);
    Task<IEnumerable<RoleModel>> GetAllAsync();
}
