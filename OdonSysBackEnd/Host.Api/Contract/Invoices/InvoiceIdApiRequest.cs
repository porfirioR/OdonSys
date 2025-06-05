using Contract.Payment.Invoices;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Contract.Invoices;

public class InvoiceIdApiRequest : IValidatableObject
{
    [Required]
    [FromRoute(Name = "id")]
    public string InvoiceId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        var invoiceManager = (IInvoiceManager)validationContext.GetService(typeof(IInvoiceManager));
        var isInvoiceIdValid = invoiceManager.IsValidInvoiceIdAsync(InvoiceId).GetAwaiter().GetResult();
        if (!isInvoiceIdValid)
        {
            results.Add(new ValidationResult($"Factura Id: {InvoiceId} es inválido o no existe."));
        }
        return results;
    }
}
