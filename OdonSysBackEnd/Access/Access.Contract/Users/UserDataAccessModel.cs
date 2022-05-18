using Utilities.Enums;

namespace Access.Contract.Users
{
    public class UserDataAccessModel
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
