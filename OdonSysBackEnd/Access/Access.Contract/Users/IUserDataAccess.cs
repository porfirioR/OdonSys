using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.Users
{
    public interface IUserDataAccess
    {
        Task<UserDataAccessModel> CreateAsync(UserDataAccessRequest dataAccess);
        Task<UserDataAccessModel> DeleteAsync(string id);
        Task<IEnumerable<UserDataAccessModel>> GetAll();
        Task<UserDataAccessModel> GetById(string id);
        Task<UserDataAccessModel> UpdateAsync(UserDataAccessRequest dataAccess);
    }
}
