namespace Contract.Administration.Users
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool Active { get; set; }
        public bool Approved { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
