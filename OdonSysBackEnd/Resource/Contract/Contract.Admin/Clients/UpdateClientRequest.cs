namespace Contract.Admin.Clients
{
    public class UpdateClientRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
