using Utilities.Enums;

namespace Access.Contract.Users
{
    public class UserDataAccessRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public string Document { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Country Country { get; set; }
        public bool Active { get; set; }
    }
}
