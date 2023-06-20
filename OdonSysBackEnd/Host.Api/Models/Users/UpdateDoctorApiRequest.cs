using System.ComponentModel.DataAnnotations;
using Utilities.Enums;

namespace Host.Api.Models.Users
{
    public class UpdateDoctorApiRequest : IValidatableObject
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public string MiddleName { get; set; }

        [Required]
        [MaxLength(25)]
        public string Surname { get; set; }

        public string SecondSurname { get; set; }

        [Required]
        [MaxLength(15)]
        public string Document { get; set; }

        [Required]
        [MaxLength(15)]
        public string Phone { get; set; }
        [Required]
        public Country Country { get; set; }
        public bool Active { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
