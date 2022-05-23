using Access.Contract.Users;

namespace Access.Contract.Auth
{
    public class AuthAccessModel
    {
        public UserDataAccessModel User { get; set; }
        public string Token { get; set; }
    }
}
