using Access.Contract.Users;

namespace Access.Contract.Authentication
{
    public record AuthenticationAccessModel
    (
        UserDataAccessModel User,
        string Token,
        DateTime ExpirationDate,
        string Scheme
    );
}
