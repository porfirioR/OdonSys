using Contract.Workspace.Procedures;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
            var procedureManager = (IProcedureManager)validationContext.GetService(typeof(IProcedureManager));
            var validName = procedureManager.ValidateIdNameAsync(Name).GetAwaiter().GetResult();
            if (!validName)
            {
                results.Add(new ValidationResult($"Nombre {Name} ya esta en uso."));
            }
            var invalidTeeth = procedureManager.ValidateProcedureTeethAsync(ProcedureTeeth).GetAwaiter().GetResult();
            if (invalidTeeth.Any())
            {
                foreach (var id in invalidTeeth)
                {
                    results.Add(new ValidationResult($"El identificaor {id} no existe."));
                }
            }
            return results;
        }
    }
}
