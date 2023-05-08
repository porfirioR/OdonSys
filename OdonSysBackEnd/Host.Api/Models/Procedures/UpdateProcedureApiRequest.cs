using Contract.Workspace.Procedures;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Models.Procedures
{
    public class UpdateProcedureApiRequest : IValidatableObject
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Longitud máxima de nombre es 100.")]
        public string Description { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor ingrese un precio válido")]
        public int Price { get; set; }
        //[Required]
        public IEnumerable<string> ProcedureTeeth { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var procedureManager = (IProcedureManager)validationContext.GetService(typeof(IProcedureManager));
            var validId = procedureManager.ValidateIdNameAsync(Id).GetAwaiter().GetResult();
            if (!validId)
            {
                results.Add(new ValidationResult($"Identificador {Id} no existe."));
            }
            //if (!ProcedureTeeth.Any())
            //{
            //    results.Add(new ValidationResult($"No se ha seleccionado ningún diente."));
            //}
            //else

            //{
            //    var invalidTeeth = procedureManager.ValidateProcedureTeethAsync(ProcedureTeeth).GetAwaiter().GetResult();
            //    if (invalidTeeth.Any())
            //    {
            //        foreach (var id in invalidTeeth)
            //        {
            //            results.Add(new ValidationResult($"El identificador del diente {id} no existe."));
            //        }
            //    }
            //}
            return results;
        }
    }
}
