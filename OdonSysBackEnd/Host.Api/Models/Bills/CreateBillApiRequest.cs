using System.ComponentModel.DataAnnotations;
using Utilities.Enums;

namespace Host.Api.Models.Bills
{
    public class CreateBillApiRequest : IValidatableObject
    {
        [Required]
        public string BillNumber { get; set; }
        [Required]
        public int Iva10 { get; set; }
        [Required]
        public int TotalIva { get; set; }
        [Required]
        public int SubTotal { get; set; }
        [Required]
        public int Total { get; set; }
        [Required]
        public string Timbrado { get; set; }
        [Required]
        public BillStatus Status { get; set; }
        [Required]
        public Guid ClientId { get; set; }
        [Required]
        public IEnumerable<BillDetailApiRequest> BillDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
