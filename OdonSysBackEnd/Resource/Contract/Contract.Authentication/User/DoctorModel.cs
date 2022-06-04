using Utilities.Enums;

namespace Contract.Workspace.User
{
    public class DoctorModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MiddleLastName { get; set; }
        public string Document { get; set; }
        public Country Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public bool Approved { get; set; }
    }
}
