using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.Users
{
    public interface IUserDataAccess
    {
        Task<IEnumerable<DoctorDataAccessModel>> GetAllAsync();
        Task<IEnumerable<UserDataAccessModel>> GetAllUserActiveAsync();
        Task<DoctorDataAccessModel> GetByIdAsync(string id);
        Task<UserDataAccessModel> ApproveNewUserAsync(string id);
        Task<DoctorDataAccessModel> UpdateAsync(UserDataAccessRequest dataAccess);
    }
}
