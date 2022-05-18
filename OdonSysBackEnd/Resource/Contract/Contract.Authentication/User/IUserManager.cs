namespace Contract.Authentication.User
{
    public interface IUserManager
    {
        Task<UserModel> Create(RegisterUserRequest createUserRequest);
        Task<UserModel> Delete(string id);
        Task<IEnumerable<UserModel>> GetAll();
        Task<UserModel> GetById(string id);
        Task<UserModel> Update(UpdateUserRequest updateUserRequest);
        Task<UserModel> Login(LoginRequest login);
    }
}
