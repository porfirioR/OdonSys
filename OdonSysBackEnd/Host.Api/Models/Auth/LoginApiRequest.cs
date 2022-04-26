using System.ComponentModel.DataAnnotations;

namespace OdonSysBackEnd.Models.Auth
{
    public class LoginApiRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
