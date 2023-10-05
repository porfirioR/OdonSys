using Contract.Administration.Authentication;
using System.Security.Claims;

namespace Contract.Administration.Users
{
    public interface IUserManager
    {
        Task<UserModel> ApproveNewUserAsync(string id);
        Task<IEnumerable<string>> CheckUsersExistsAsync(IEnumerable<string> users);
        Task<IEnumerable<DoctorModel>> GetAllAsync();
        Task<IEnumerable<string>> SetUserRolesAsync(UserRolesRequest request);
        Task<DoctorModel> GetByIdAsync(string id);
        Task<UserClientModel> GetUserClientAsync(string userId, string clientId);
        Task<AuthenticationModel> LoginAsync(string authorization);
        Task<AuthenticationModel> RegisterUserAsync(RegisterUserRequest createUserRequest);
        Task<DoctorModel> UpdateAsync(UpdateDoctorRequest updateUserRequest);
        bool RemoveAllClaims(ClaimsPrincipal claimsPrincipal);
        Task<UserModel> GetUserFromGraphApiByIdAsync(string id);
        Task<IEnumerable<DoctorModel>> GetAllUsersByAzureAsync();
        Task<UserModel> RegisterUserAsync(string userId);
    }
}
