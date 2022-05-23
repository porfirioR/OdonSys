using Access.Contract.Users;
using System.Threading.Tasks;

namespace Access.Contract.Auth
{
    public interface IAuthDataAccess
    {
        Task<AuthAccessModel> LoginAsync(LoginDataAccess loginAccess);
        Task<UserDataAccessModel> RegisterUserAsync(UserDataAccessRequest dataAccess);
    }
}
