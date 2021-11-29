namespace Access.Contract.Clients
{
    public class UpdateClientAccessRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public bool Debts { get; set; }
    }
}
