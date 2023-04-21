using System.ComponentModel.DataAnnotations;
using Utilities.Enums;

namespace Host.Api.Models.Auth
{
    public class RegisterUserApiRequest
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(25)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(25)]
        public string Surname { get; set; }

        [MaxLength(25)]
        public string SecondSurname { get; set; }

        [Required]
        [MaxLength(15)]
        public string Document { get; set; }

        [Required]
        [MaxLength(25)]
        public string Password { get; set; }

        [Required]
        [MaxLength(15)]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(40)]
        public string Email { get; set; }

        [Required]
        public Country Country { get; set; }
    }
}
