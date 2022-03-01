using Utilities.Enums;

namespace Contract.Admin.User
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public Country Country { get; set; }
    }
}
