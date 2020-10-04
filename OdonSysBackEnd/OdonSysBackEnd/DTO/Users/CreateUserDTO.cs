using System;
using System.ComponentModel.DataAnnotations;

namespace OdonSys.Api.Main.DTO.Users
{
    public class CreateUserDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public bool Active { get; set; }
    }
}
