using Utilities.Enums;

namespace OdonSysBackEnd.Models.Clients
{
    public class UpdateClientApiRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public Country Country { get; set; }
    }
}
