using Contract.Workspace.Procedures;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Models.Invoices
{
    public class CreateInvoiceDetailApiRequest : IValidatableObject
    {
        [Required]
        public string ClientProcedureId { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor ingrese un precio de procedimiento válido")]
        public int ProcedurePrice { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor ingrese un monto total válido")]
        public int FinalPrice { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var procedureManager = (IProcedureManager)validationContext.GetService(typeof(IProcedureManager));
            var validProcedureId = procedureManager.CheckExistsClientProcedureAsync(ClientProcedureId).GetAwaiter().GetResult();
            if (!validProcedureId)
            {
                results.Add(new ValidationResult($"Id del cliente procedimiento {ClientProcedureId} ingresado es inválido."));
            }

            if (FinalPrice > ProcedurePrice)
            {
                results.Add(new ValidationResult($"El valor final del procedimiento {FinalPrice} es mayor al precio referencia {ProcedurePrice}."));
            }
            return results;
        }
    }
}
