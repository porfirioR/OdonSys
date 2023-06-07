using Access.Contract.Users;

namespace Access.Contract.Auth
{
    public record AuthAccessModel
    (
        UserDataAccessModel User,
        string Token,
        DateTime ExpirationDate,
        string Scheme
    );
}
