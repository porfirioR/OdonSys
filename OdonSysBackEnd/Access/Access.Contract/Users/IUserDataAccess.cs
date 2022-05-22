using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.Users
{
    public interface IUserDataAccess
    {
        Task<UserDataAccessModel> DeleteAsync(string id);
        Task<IEnumerable<UserDataAccessModel>> GetAll();
        Task<UserDataAccessModel> GetByIdAsync(string id);
        Task<UserDataAccessModel> ApproveNewUserAsync(string id);
        Task<UserDataAccessModel> UpdateAsync(UserDataAccessRequest dataAccess);
    }
}
