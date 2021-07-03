using Access.Contract.Auth;
using System.Threading.Tasks;

namespace Access.Contract
{
    public interface IAuthDataAccess
    {
        Task<AuthResponse> Login(LoginDataAccess loginAccess);
    }
}
