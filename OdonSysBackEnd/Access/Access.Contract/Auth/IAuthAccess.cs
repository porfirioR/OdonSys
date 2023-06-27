using Access.Contract.Users;
using System.Security.Claims;

namespace Access.Contract.Auth
{
    public interface IAuthAccess
    {
        Task<AuthAccessModel> LoginAsync(LoginDataAccess loginAccess);
        Task<AuthAccessModel> RegisterUserAsync(UserDataAccessRequest dataAccess);
        bool RemoveAllClaims(ClaimsPrincipal claimsPrincipal);
    }
}
