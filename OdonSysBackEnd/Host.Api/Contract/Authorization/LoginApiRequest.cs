using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Host.Api.Contract.Authorization
{
    public class LoginApiRequest : IValidatableObject
    {
        [Required]
        [FromHeader]
        public string Authorization { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (Authorization.StartsWith("basic ", StringComparison.OrdinalIgnoreCase))
            {
                var encodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(Authorization["Basic ".Length..]));
                var credentials = encodedCredentials.Split(":");
                if (credentials.Length != 2)
                {
                    results.Add(new ValidationResult("Credenciales incorrectas."));
                }
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

            return results;
        }
    }
}
