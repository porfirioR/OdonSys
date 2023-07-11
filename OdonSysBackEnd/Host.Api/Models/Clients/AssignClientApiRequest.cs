using Contract.Administration.Clients;
using Contract.Administration.Users;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Models.Clients
{
    public class AssignClientApiRequest : IValidatableObject
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ClientId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var userManager = (IUserManager)validationContext.GetService(typeof(IUserManager));
            var user = userManager.GetByIdAsync(UserId).GetAwaiter().GetResult();
            if (!(user.Active && user.Approved))
            {
                results.Add(new ValidationResult($"El doctor con id: {UserId} está inactivo o no tiene accesos al sistema."));
            }
            var clientManager = (IClientManager)validationContext.GetService(typeof(IClientManager));
            _ = clientManager.GetByIdAsync(ClientId).GetAwaiter().GetResult();
            var clients = clientManager.GetClientsByUserIdAsync(UserId, "").GetAwaiter().GetResult();
            if (clients.Any(x => x.Id == ClientId))
            {
                results.Add(new ValidationResult($"El doctor ya tiene al paciente en su lista."));
            }
            return results;
        }
    }
}
