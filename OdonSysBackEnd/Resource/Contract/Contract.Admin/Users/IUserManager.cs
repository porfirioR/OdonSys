using Contract.Admin.Auth;
using System.Security.Claims;

namespace Contract.Admin.Users
{
    public interface IUserManager
    {
        Task<UserModel> ApproveNewUserAsync(string id);
        Task<IEnumerable<string>> CheckUsersExistsAsync(IEnumerable<string> users);
        Task<IEnumerable<DoctorModel>> GetAllAsync();
        Task<IEnumerable<string>> SetUserRolesAsync(UserRolesRequest request, ClaimsPrincipal claimsPrincipal);
        Task<DoctorModel> GetByIdAsync(string id);
        Task<UserClientModel> GetUserClientAsync(string userId, string clientId);
        Task<AuthModel> LoginAsync(string authorization);
        Task<AuthModel> RegisterUserAsync(RegisterUserRequest createUserRequest);
        Task<DoctorModel> UpdateAsync(UpdateDoctorRequest updateUserRequest);
        bool RemoveAllClaims(ClaimsPrincipal claimsPrincipal);
    }
}
