using Contract.Authentication.User;

namespace Contract.Workspace.User
{
    public interface IUserManager
    {
        Task<AuthModel> RegisterUserAsync(RegisterUserRequest createUserRequest);
        Task<AuthModel> LoginAsync(LoginRequest login);
        Task<UserModel> DeactivateAsync(string id);
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<UserModel> GetByIdAsync(string id);
        Task<UserModel> UpdateAsync(UpdateUserRequest updateUserRequest);
        Task<UserModel> ApproveNewUserAsync(string id);
    }
}
