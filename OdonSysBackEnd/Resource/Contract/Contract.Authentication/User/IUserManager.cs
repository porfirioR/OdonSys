using Contract.Authentication.User;

namespace Contract.Workspace.User
{
    public interface IUserManager
    {
        Task<AuthModel> RegisterUserAsync(RegisterUserRequest createUserRequest);
        Task<AuthModel> LoginAsync(LoginRequest login);
        Task<UserModel> DeactivateAsync(string id);

        Task<IEnumerable<DoctorModel>> GetAllAsync();
        Task<DoctorModel> GetByIdAsync(string id);
        Task<DoctorModel> UpdateAsync(UpdateUserRequest updateUserRequest);
        Task<DoctorModel> ApproveNewUserAsync(string id);
    }
}
