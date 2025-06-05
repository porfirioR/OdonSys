using Access.Contract.Users;
using System.Security.Claims;

namespace Access.Contract.Authentication;

public interface IAuthenticationAccess
{
    Task<AuthenticationAccessModel> LoginAsync(LoginDataAccess loginAccess);
    Task<AuthenticationAccessModel> RegisterUserAsync(UserDataAccessRequest dataAccess);
    Task<UserDataAccessModel> RegisterAzureAdB2CUserAsync(UserDataAccessRequest dataAccess);
    bool RemoveAllClaims(ClaimsPrincipal claimsPrincipal);
}
