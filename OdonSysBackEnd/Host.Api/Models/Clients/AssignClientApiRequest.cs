using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Utilities.Enums;

namespace Host.Api.Models.Clients
{
    public class AssignClientApiRequest
    {
        [Required]
        public string DoctorId { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public DoctorClientStatus Status { get; set; }
        public IFormFile File { get; set; }
    }
}
