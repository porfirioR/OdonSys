using Contract.Pyment.Bills;
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
            var billManager = (IBillManager)validationContext.GetService(typeof(IBillManager));
            var isValidId = billManager.IsValidBillIdAsync(Id).GetAwaiter().GetResult();
            if (!isValidId)
            {
                results.Add(new ValidationResult($"{Id} es inválido o no existe."));
            }
            return results;
        }
    }
}
