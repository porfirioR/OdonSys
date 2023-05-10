using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Models.Payments
{
    public class HeaderPaymentApiRequest : IValidatableObject
    {
        [Required]
        [FromHeader]
        public string Id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            throw new NotImplementedException();
        }
    }
}
