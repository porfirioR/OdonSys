namespace Contract.Admin.Roles
{
    public interface IRoleManager
    {
        Task<IEnumerable<string>> GetPermissonsByRolesAsync(IEnumerable<string> roles);
        IEnumerable<PermissionModel> GetAllPermissions();
        Task<RoleModel> CreateAsync(CreateRoleRequest request);
        Task<RoleModel> UpdateAsync(UpdateRoleRequest request);
        Task<RoleModel> GetRoleByCodeAsync(string code);
        Task<IEnumerable<RoleModel>> GetAllAsync();
    }
}
