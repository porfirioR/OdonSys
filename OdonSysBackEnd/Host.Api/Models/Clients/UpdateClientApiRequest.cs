using Contract.Admin.Clients;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public string LastName { get; set; }
        public string MiddleLastName { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var clientManager = (IClientManager)validationContext.GetService(typeof(IClientManager));
            var client = clientManager.GetByIdAsync(Id).GetAwaiter().GetResult();
            if (client is null)
            {
                results.Add(new ValidationResult($"Ciente con Id {Id} no existe o esta inactivo."));
            }
            return results;
        }
    }
}
