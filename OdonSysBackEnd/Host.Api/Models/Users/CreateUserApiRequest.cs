using System.ComponentModel.DataAnnotations;
using Utilities.Enums;

namespace Host.Api.Models.Users
{
    public class CreateUserApiRequest
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(15)]
        public string Document { get; set; }

        [Required]
        public Country Country { get; set; }
    }
}
