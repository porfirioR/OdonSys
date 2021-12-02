using Utilities.Enums;

namespace Access.Contract.Clients
{
    public class CreateClientAccessRequest
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Document { get; set; }
        public string Ruc { get; set; }
        public Country Country { get; set; }
        public bool Debts { get; set; }
        public string Phone { get; set; }
    }
}
