using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Utilities.Enums;

namespace OdonSysBackEnd.Models.Clients
{
    public class CreateClientApiRequest : IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        public string SecondName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        [Required]
        public string Document { get; set; }
        public string Ruc { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public Country Country { get; set; }
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
