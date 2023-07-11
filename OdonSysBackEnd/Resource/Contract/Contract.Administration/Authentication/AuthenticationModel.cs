using Contract.Administration.Users;

namespace Contract.Administration.Authentication
{
    public class AuthenticationModel
    {
        public UserModel User { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Scheme { get; set; }
    }
}
