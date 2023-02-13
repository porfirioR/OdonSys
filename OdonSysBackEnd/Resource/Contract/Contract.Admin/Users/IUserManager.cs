using Contract.Admin.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.Admin.Users
{
    public interface IUserManager
    {
        Task<UserModel> ApproveNewUserAsync(string id);
        Task<IEnumerable<string>> CheckUsersExistsAsync(IEnumerable<string> users);
        Task<IEnumerable<DoctorModel>> GetAllAsync();
        Task<DoctorModel> GetByIdAsync(string id);
        Task<AuthModel> LoginAsync(string authorization);
        Task<AuthModel> RegisterUserAsync(RegisterUserRequest createUserRequest);
        Task<DoctorModel> UpdateAsync(UpdateDoctorRequest updateUserRequest);
    }
}
