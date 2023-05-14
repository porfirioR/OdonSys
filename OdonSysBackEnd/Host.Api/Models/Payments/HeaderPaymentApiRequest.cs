using Contract.Pyment.Invoices;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Models.Payments
{
    public class HeaderPaymentApiRequest : IValidatableObject
    {
        [Required]
        [FromRoute]
        public string Id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var invoiceManager = (IInvoiceManager)validationContext.GetService(typeof(IInvoiceManager));
            var isValidId = invoiceManager.IsValidInvoiceIdAsync(Id).GetAwaiter().GetResult();
            if (!isValidId)
            {
                results.Add(new ValidationResult($"{Id} es inválido o no existe."));
            }
            return results;
        }
    }
}
