using Contract.Authentication.User;

namespace Contract.Workspace.User
{
    public interface IUserManager
    {
        Task<UserModel> RegisterUserAsync(RegisterUserRequest createUserRequest);
        Task<UserModel> DeactivateAsync(string id);
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<UserModel> GetByIdAsync(string id);
        Task<UserModel> UpdateAsync(UpdateUserRequest updateUserRequest);
        Task<UserModel> LoginAsync(LoginRequest login);
        Task<UserModel> ApproveNewUserAsync(string id);
    }
}
