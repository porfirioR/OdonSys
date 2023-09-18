using Contract.Payment.Invoices;
using Contract.Payment.Payments;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Contract.Invoices
{
    public class UpdateApiRequest : IValidatableObject
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor ingrese iva válido")]
        public int Iva10 { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor ingrese total iva válido")]
        public int TotalIva { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor ingrese un monto sub total válido")]
        public int SubTotal { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor ingrese un monto total válido")]
        public int Total { get; set; }
        [Required]
        public IEnumerable<UpdateInvoiceDetailApiRequest> InvoiceDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var invoiceManager = (IInvoiceManager)validationContext.GetService(typeof(IInvoiceManager));
            var paymentManager = (IPaymentManager)validationContext.GetService(typeof(IPaymentManager));

            var isValidInvoice = invoiceManager.IsValidInvoiceIdAsync(Id).GetAwaiter().GetResult();
            if (!isValidInvoice)
            {
                results.Add(new ValidationResult($"Identificador {Id} no existe o ha sido modificado."));
                return results;
            }
            var invoiceDetailIds = InvoiceDetails.Select(x => x.Id);
            var savedInvoice = invoiceManager.GetInvoiceByIdAsync(Id).GetAwaiter().GetResult();
            if (savedInvoice.Status == Utilities.Enums.InvoiceStatus.Completado)
            {
                results.Add(new ValidationResult($"La Factura con el Identificador {Id} no puede ser modificada."));
                return results;
            }
            var savedInvoiceDetailIds = savedInvoice.InvoiceDetails.Select(x => x.Id.ToString());
            var invalidInvoiceDetailIds = invoiceDetailIds.Where(x => !savedInvoiceDetailIds.Contains(x));
            foreach (var invalidInvoiceDetailId in invalidInvoiceDetailIds)
            {
                results.Add(new ValidationResult($"El Identificador {invalidInvoiceDetailId} no existe o ha sido modificado."));
            }
            var allPayments = paymentManager.GetPaymentsByInvoiceIdAsync(Id).GetAwaiter().GetResult();
            var amountPayments = allPayments.Sum(x => x.Amount);
            var amountInvoices = InvoiceDetails.Sum(x => x.FinalPrice);
            if (amountPayments > amountInvoices)
            {
                results.Add(new ValidationResult($"La sumatoria de los montos ingresados: {amountInvoices} debe ser menor o igual a la sumatoria total de montos ya abonados: {amountPayments}."));
            }
            return results;
        }
    }
}
