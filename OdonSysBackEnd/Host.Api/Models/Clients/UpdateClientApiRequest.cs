using Contract.Admin.Clients;
using System.ComponentModel.DataAnnotations;
using Utilities.Enums;

namespace Host.Api.Models.Clients
{
    public class UpdateClientApiRequest : IValidatableObject
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public Country Country { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public bool Active { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var clientManager = (IClientManager)validationContext.GetService(typeof(IClientManager));
            _ = clientManager.GetByIdAsync(Id).GetAwaiter().GetResult();
            return results;
        }
    }
}
