using Contract.Admin.Users;

namespace Contract.Admin.Auth
{
    public class AuthModel
    {
        public UserModel User { get; set; }
        public string Token { get; set; }
        public string Scheme { get; set; }
    }
}
