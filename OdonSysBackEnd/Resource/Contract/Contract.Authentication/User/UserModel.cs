using Utilities.Enums;

namespace Contract.Authentication.User
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Country Country { get; set; }
    }
}
