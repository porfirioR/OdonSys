using Contract.Pyment.Bills;
using Contract.Pyment.Payments;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Host.Api.Models.Payments
{
    public class PaymentApiRequest : IValidatableObject
    {
        [Required]
        public string HeaderBillId { set; get; }
        [Required]
        public string UserId { set; get; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor ingrese un monto válido")]
        public int Amount { set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var billManager = (IBillManager)validationContext.GetService(typeof(IBillManager));
            var paymentManager = (IPaymentManager)validationContext.GetService(typeof(IPaymentManager));
            var isValidId = billManager.IsValidBillIdAsync(HeaderBillId).GetAwaiter().GetResult();
            if (!isValidId)
            {
                results.Add(new ValidationResult($"{HeaderBillId} es inválido o no existe."));
            }
            var bill = billManager.GetBillsAsync().GetAwaiter().GetResult().First(x => x.Id == new Guid(HeaderBillId));
            var billPayments = paymentManager.GetPaymentsByBillIdAsync(HeaderBillId).GetAwaiter().GetResult();
            if (Amount > bill.Total)
            {
                results.Add(new ValidationResult($"{Amount} supera el monto de la factura {bill.Total}."));
            }
            var activeDebts = bill.Total - billPayments.Select(x => x.Amount).Sum();
            if (Amount > activeDebts)
            {
                results.Add(new ValidationResult($"{Amount} supera la deuda faltante {activeDebts}."));
            }
            return results;
        }
    }
}
