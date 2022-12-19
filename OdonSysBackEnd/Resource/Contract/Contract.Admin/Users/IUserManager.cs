using Contract.Admin.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.Admin.Users
{
    public interface IUserManager
    {
        Task<AuthModel> RegisterUserAsync(RegisterUserRequest createUserRequest);
        Task<AuthModel> LoginAsync(string authorization);
        Task<UserModel> ApproveNewUserAsync(string id);

        Task<IEnumerable<DoctorModel>> GetAllAsync();
        Task<DoctorModel> GetByIdAsync(string id);
        Task<DoctorModel> UpdateAsync(UpdateDoctorRequest updateUserRequest);
    }
}
