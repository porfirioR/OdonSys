using Utilities.Enums;

namespace OdonSysBackEnd.Models.Clients
{
    public class CreateClientApiRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocumentId { get; set; }
        public Country Country { get; set; }
    }
}
