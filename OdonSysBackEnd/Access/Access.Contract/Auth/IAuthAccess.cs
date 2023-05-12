using Access.Contract.Users;

namespace Access.Contract.Auth
{
    public interface IAuthAccess
    {
        Task<AuthAccessModel> LoginAsync(LoginDataAccess loginAccess);
        Task<AuthAccessModel> RegisterUserAsync(UserDataAccessRequest dataAccess);
    }
}
