using Contract.Admin.Clients;
using Contract.Workspace.Procedures;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Models.ClientProcedures
{
    public class CreateClientProcedureApiRequest : IValidatableObject
    {
        [Required]
        public string ProcedureId { get; set; }
        [Required]
        public string ClientId { get; set; }
        //[Required]
        //[Range(0, int.MaxValue, ErrorMessage = "Por favor ingrese un precio válido")]
        //public int Price { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var procedureManager = (IProcedureManager)validationContext.GetService(typeof(IProcedureManager));
            var clientManager = (IClientManager)validationContext.GetService(typeof(IClientManager));
            _ = procedureManager.GetByIdAsync(ProcedureId, true).GetAwaiter().GetResult();
            _ = clientManager.GetByIdAsync(ClientId).GetAwaiter().GetResult();
            return results;
        }
    }
}
