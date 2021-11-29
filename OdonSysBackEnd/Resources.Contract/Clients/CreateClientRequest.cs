using Utilities.Enums;

namespace Resources.Contract.Clients
{
    public class CreateClientRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocumentId { get; set; }
        public Country Country { get; set; }
    }
}
