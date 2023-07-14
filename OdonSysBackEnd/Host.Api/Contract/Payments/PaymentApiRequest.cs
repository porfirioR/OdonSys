using Contract.Payment.Invoices;
using Contract.Payment.Payments;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Contract.Payments
{
    public class PaymentApiRequest : IValidatableObject
    {
        [Required]
        public string InvoiceId { set; get; }
        [Required]
        public string UserId { set; get; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor ingrese un monto válido")]
        public int Amount { set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var invoiceManager = (IInvoiceManager)validationContext.GetService(typeof(IInvoiceManager));
            var paymentManager = (IPaymentManager)validationContext.GetService(typeof(IPaymentManager));
            var isValidId = invoiceManager.IsValidInvoiceIdAsync(InvoiceId).GetAwaiter().GetResult();
            if (!isValidId)
            {
                results.Add(new ValidationResult($"{InvoiceId} es inválido o no existe."));
            }
            var invoice = invoiceManager.GetInvoicesAsync().GetAwaiter().GetResult().First(x => x.Id == new Guid(InvoiceId));
            var invoicePayments = paymentManager.GetPaymentsByInvoiceIdAsync(InvoiceId).GetAwaiter().GetResult();
            if (Amount > invoice.Total)
            {
                results.Add(new ValidationResult($"{Amount} supera el monto de la factura {invoice.Total}."));
            }
            var activeDebts = invoice.Total - invoicePayments.Select(x => x.Amount).Sum();
            if (Amount > activeDebts)
            {
                results.Add(new ValidationResult($"{Amount} supera la deuda faltante {activeDebts}."));
            }
            return results;
        }
    }
}
