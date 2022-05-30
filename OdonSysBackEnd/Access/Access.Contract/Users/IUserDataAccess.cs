using System.Collections.Generic;
using System.Threading.Tasks;

namespace Access.Contract.Users
{
    public interface IUserDataAccess
    {
        Task<DoctorDataAccessModel> DeleteAsync(string id);
        Task<IEnumerable<DoctorDataAccessModel>> GetAll();
        Task<DoctorDataAccessModel> GetByIdAsync(string id);
        Task<UserDataAccessModel> ApproveNewUserAsync(string id);
        Task<DoctorDataAccessModel> UpdateAsync(UserDataAccessRequest dataAccess);
    }
}
