using Utilities.Enums;

namespace Contract.Admin.Users
{
    public class RegisterUserRequest
    {
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MiddleLastName { get; set; }
        public string Document { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Country Country { get; set; }
    }
}
