namespace Access.Contract.Users
{
    public class UserDataAccessModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool Active { get; set; }
        public bool Approved { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public UserDataAccessModel(
            string id,
            string userName,
            bool active,
            bool approved,
            IEnumerable<string> roles
        )
        {
            Id = id;
            UserName = userName;
            Active = active;
            Approved = approved;
            Roles = roles;
        }
    }
}
