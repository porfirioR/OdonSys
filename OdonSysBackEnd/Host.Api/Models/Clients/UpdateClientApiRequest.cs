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
            var clientModel = clientManager.GetByIdAsync(Id).GetAwaiter().GetResult();
            if (!clientModel.Active)
            {
                results.Add(new ValidationResult($"Ciente con Id {Id} esta inactivo."));
            }
            return results;
        }
    }
}
