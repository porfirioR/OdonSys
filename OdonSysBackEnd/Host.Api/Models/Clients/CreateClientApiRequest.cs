using Contract.Admin.Clients;
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
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
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
            var documentLength = Document.Length;
            if (documentLength < 5)
            {
                results.Add(new ValidationResult($"Longitud mínima de documento es 5."));
            }
            else if (documentLength > 10)
            {
                results.Add(new ValidationResult($"Longitud máxima de documento es 10."));
            }
            var clientManager = (IClientManager)validationContext.GetService(typeof(IClientManager));
            if (clientManager.GetByDocumentAsync(Document).GetAwaiter().GetResult() != null)
            {
                results.Add(new ValidationResult($"Paciente con el document: {Document} ya existe."));
            }
            if (clientManager.IsDuplicateEmailAsync(Email).GetAwaiter().GetResult())
            {
                results.Add(new ValidationResult($"El correo {Email} ya existe."));
            }
            if (Country != Country.Paraguay && !string.IsNullOrEmpty(Ruc))
            {
                results.Add(new ValidationResult($"Valor ingresado en ruc: {Ruc} es inválido."));
            }
            var checkDigit = Country == Country.Paraguay ? CalculateParaguayanCheckDigit() : -1;
            if (Country == Country.Paraguay && Ruc != checkDigit.ToString())
            {
                results.Add(new ValidationResult($"Valor ingresado en ruc: {Ruc} es inválido."));
            }
            else if (Country != Country.Paraguay && checkDigit.ToString() != "-1")
            {
                results.Add(new ValidationResult($"Valor ingresado en ruc: {Ruc} es inválido."));
            }
            return results;
        }

        private int CalculateParaguayanCheckDigit()
        {
            var multiplier = 2;
            var module = 11;
            var documentReverse = Document.ToCharArray();
            Array.Reverse(documentReverse);
            var result = 0;
            foreach (var value in documentReverse)
            {
                result += multiplier * (int)char.GetNumericValue(value);
                multiplier++;
                if (multiplier > module)
                {
                    multiplier = 2;
                }
            }
            var rest = result % module;
            var checkDigit = rest > 1 ? module - rest : 0;
            return checkDigit;
        }
    }
}
