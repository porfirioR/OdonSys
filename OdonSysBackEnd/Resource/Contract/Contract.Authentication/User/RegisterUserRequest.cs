using Utilities.Enums;

namespace Contract.Authentication.User
{
    public class RegisterUserRequest
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Document { get; set; }
        public Country Country { get; set; }
    }
}
