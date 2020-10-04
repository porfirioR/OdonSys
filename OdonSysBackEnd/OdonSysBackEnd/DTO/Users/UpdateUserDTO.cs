using System.ComponentModel.DataAnnotations;

namespace OdonSys.Api.Main.DTO.Users
{
    public class UpdateUserDTO : CreateUserDTO
    {
        [Key]
        public string Id { get; set; }
    }
}
