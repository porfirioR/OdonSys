using System.ComponentModel.DataAnnotations;

namespace Host.Api.Models.Clients
{
    public class AssignClientApiRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ClientId { get; set; }
    }
}
