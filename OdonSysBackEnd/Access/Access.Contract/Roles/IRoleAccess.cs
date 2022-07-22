using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.Roles
{
    public interface IRoleAccess
    {
        Task<RoleAccessModel> CreateAccessAsync(CreateRoleAccessRequest request);
        Task<RoleAccessModel> UpdateAccessAsync(UpdateRoleAccessModel request);
        Task<RoleAccessModel> GetRoleByCodeAccessAsync(string code);
        Task<IEnumerable<RoleAccessModel>> GetAllAccessAsync();
    }
}
