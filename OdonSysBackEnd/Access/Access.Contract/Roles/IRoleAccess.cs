using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.Roles
{
    public interface IRoleAccess
    {
        Task<RoleAccessModel> CreateAccessAsync(CreateRoleAccessRequest accessRequest);
        Task<RoleAccessModel> UpdateAccessAsync(UpdateRoleAccessRequest accessRequest);
        Task<RoleAccessModel> GetRoleByCodeAccessAsync(string code);
        Task<IEnumerable<RoleAccessModel>> GetAllAccessAsync();
    }
}
