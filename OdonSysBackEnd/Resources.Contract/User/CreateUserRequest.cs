using Utilities.Enums;

namespace Resources.Contract.User
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public Country Country { get; set; }
    }
}
