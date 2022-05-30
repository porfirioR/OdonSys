using Contract.Authentication.User;

namespace Contract.Workspace.User
{
    public class AuthModel
    {
        public UserModel User { get; set; }
        public string Token { get; set; }
    }
}
