using Contract.Workspace.Teeth;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Contract.Invoices;

public class UpdateInvoiceDetailApiRequest : IValidatableObject
{
    [Required]
    public string Id { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public int FinalPrice { get; set; }
    public IEnumerable<string> ToothIds { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        if (ToothIds.Any())
        {
            var toothManager = (IToothManager)validationContext.GetService(typeof(IToothManager));
            var invalidTeeth = toothManager.GetInvalidTeethAsync(ToothIds).GetAwaiter().GetResult();
            foreach (var tooth in invalidTeeth)
            {
                results.Add(new ValidationResult($"Id del diente {tooth} ingresado es inválido."));
            }
        }

        return results;
    }
}
