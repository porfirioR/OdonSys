using Contract.Administration.Users;

namespace Contract.Administration.Authentication
{
    public record AuthenticationModel
    (
        UserModel User,
        string Token,
        DateTime ExpirationDate,
        string Scheme
    );
}
