using Contract.Admin.Clients;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Utilities.Enums;

namespace Host.Api.Models.Clients
{
    public class CreateClientApiRequest : IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleLastName { get; set; }
        [Required]
        public string Document { get; set; }
        public string Ruc { get; set; }
        [Required]
        public Country Country { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var clientManager = (IClientManager)validationContext.GetService(typeof(IClientManager));
            if (clientManager.GetByDocumentAsync(Document).GetAwaiter().GetResult() != null)
            {
                results.Add(new ValidationResult($"Paciente con el document: {Document} ya existe."));
            }
            // TODO: Validate Ruc if country is Paraguay
            return results;
        }
    }
}
