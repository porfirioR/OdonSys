using Utilities.Enums;

namespace Contract.Workspace.User
{
    public class UpdateUserRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public Country Country { get; set; }
    }
}
