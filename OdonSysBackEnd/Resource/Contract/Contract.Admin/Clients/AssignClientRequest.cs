using Microsoft.AspNetCore.Http;
using Utilities.Enums;

namespace Contract.Admin.Clients
{
    public class AssignClientRequest
    {
        public string DoctorId { get; set; }
        public string ClientId { get; set; }
        public DoctorClientStatus Status { get; set; }
        public IFormFile File { get; set; }
    }
}
