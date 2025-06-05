using Contract.Administration.Clients;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Contract.Orthodontics;

public class OrthodonticApiRequest : IValidatableObject
{
    [Required]
    public string Description { get; set; }
    [Required]
    public string ClientId { get; set; }
    [Required]
    public DateTime Date { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        var clientManager = (IClientManager)validationContext.GetService(typeof(IClientManager));
        var client = clientManager.GetByIdAsync(ClientId).GetAwaiter().GetResult();
        if (!client.Active)
        {
            results.Add(new ValidationResult($"El paciente con id: {ClientId} está inactivo o no existe."));
        }
        return results;
    }
}
