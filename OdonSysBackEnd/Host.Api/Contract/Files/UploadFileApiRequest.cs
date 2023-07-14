using Contract.Payment.Invoices;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Contract.Files
{
    public class UploadFileApiRequest : IValidatableObject
    {
        [Required]
        public string Id { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var invoiceManager = (IInvoiceManager)validationContext.GetService(typeof(IInvoiceManager));
            var isValidInvoice = invoiceManager.IsValidInvoiceIdAsync(Id).GetAwaiter().GetResult();
            if (!isValidInvoice)
            {
                results.Add(new ValidationResult($"Identificador {Id} no existe o ha sido modificado."));
            }
            var ivalidFiles = Files.Where(x => string.IsNullOrEmpty(Path.GetExtension(x.FileName))).Select(x => x.FileName);
            if (ivalidFiles.Any())
            {
                foreach (var file in ivalidFiles)
                {
                    results.Add(new ValidationResult($"Filename {file} extension cannot be empty."));
                }
            }
            return results;
        }
    }
}
