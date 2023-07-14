using Contract.Administration.Clients;
using System.ComponentModel.DataAnnotations;
using Utilities.Enums;

namespace Host.Api.Contract.Clients
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
        [Required]
        public string Document { get; set; }
        public bool Active { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var clientManager = (IClientManager)validationContext.GetService(typeof(IClientManager));
            _ = clientManager.GetByIdAsync(Id).GetAwaiter().GetResult();

            var documentLength = Document.Length;
            if (documentLength < 5)
            {
                results.Add(new ValidationResult($"Longitud mínima de documento es 5."));
            }
            else if (documentLength > 10)
            {
                results.Add(new ValidationResult($"Longitud máxima de documento es 10."));
            }
            if (clientManager.IsDuplicateEmailAsync(Email, Id).GetAwaiter().GetResult())
            {
                results.Add(new ValidationResult($"El correo {Email} ya fue registrado."));
            }
            if (clientManager.IsDuplicateDocumentAsync(Document, Id).GetAwaiter().GetResult())
            {
                results.Add(new ValidationResult($"El documento {Document} ya fue registrado."));
            }
            return results;
        }
    }
}
