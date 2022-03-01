using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Models.Procedures
{
    public class CreateProcedureApiRequest : IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string EstimatedSessions { get; set; }
        [Required]
        public IEnumerable<string> ProcedureTeeth { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
