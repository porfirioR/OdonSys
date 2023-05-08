using System.ComponentModel.DataAnnotations;

namespace Host.Api.Models.Bills
{
    public class BillDetailApiRequest : IValidatableObject
    {
        [Required]
        public Guid ClientProcedureId { get; set; }
        [Required]
        public int ProcedurePrice { get; set; }
        [Required]
        public int FinalPrice { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
