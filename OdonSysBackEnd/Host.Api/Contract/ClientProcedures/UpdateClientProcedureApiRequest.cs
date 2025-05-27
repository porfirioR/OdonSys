using Contract.Workspace.Procedures;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Contract.ClientProcedures;

public class UpdateClientProcedureApiRequest : IValidatableObject
{
    [Required]
    public string UserClientId { get; set; }
    [Required]
    public string ProcedureId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        var procedureManager = (IProcedureManager)validationContext.GetService(typeof(IProcedureManager));
        _ = procedureManager.GetByIdAsync(ProcedureId, true).GetAwaiter().GetResult();
        var exists = procedureManager.CheckExistsClientProcedureAsync(UserClientId, ProcedureId).GetAwaiter().GetResult();
        if (!exists)
        {
            results.Add(new ValidationResult($"No existe la relación entre {UserClientId}, y {ProcedureId}"));
        }
        return results;
    }
}
