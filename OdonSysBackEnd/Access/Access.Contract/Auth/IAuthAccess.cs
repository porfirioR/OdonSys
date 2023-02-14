using Access.Contract.Users;
using System.Threading.Tasks;

namespace Access.Contract.Auth
{
    public interface IAuthAccess
    {
        Task<AuthAccessModel> LoginAsync(LoginDataAccess loginAccess);
        Task<AuthAccessModel> RegisterUserAsync(UserDataAccessRequest dataAccess);
    }
}
