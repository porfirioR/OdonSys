using Access.Contract.Users;

namespace Access.Contract.Auth
{
    public class AuthResponse
    {
        public UserDataAccessModel User { get; set; }
        public string Token { get; set; }
        public bool DefaultPassword { get; set; }
    }
}
