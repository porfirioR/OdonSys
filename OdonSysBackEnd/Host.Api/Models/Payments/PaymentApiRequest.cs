using System.ComponentModel.DataAnnotations;

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
            return results;
        }
    }
}
