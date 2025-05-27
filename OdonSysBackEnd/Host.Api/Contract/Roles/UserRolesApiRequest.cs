using Contract.Administration.Roles;
using Contract.Administration.Users;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Contract.Roles;

public class UserRolesApiRequest : IValidatableObject
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public IEnumerable<string> Roles { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        var userManager = (IUserManager)validationContext.GetService(typeof(IUserManager));
        var roleManager = (IRoleManager)validationContext.GetService(typeof(IRoleManager));
        _ = userManager.GetByIdAsync(UserId).GetAwaiter().GetResult();
        var codeRoles = roleManager.GetAllAsync().GetAwaiter().GetResult().Select(x => x.Code);
        var invalidRoles = Roles.Where(x => !codeRoles.Contains(x));
        foreach (var codeRole in invalidRoles)
        {
            results.Add(new ValidationResult($"Rol no existe: {codeRole}"));
        }
        return results;
    }
}
