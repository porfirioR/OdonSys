using Utilities.Enums;

namespace Contract.Workspace.User
{
    public class UpdateDoctorRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MiddleLastName { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public Country Country { get; set; }
    }
}
