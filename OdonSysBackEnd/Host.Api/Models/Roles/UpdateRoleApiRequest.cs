using Contract.Administration.Roles;
using Contract.Administration.Users;
using System.ComponentModel.DataAnnotations;
using Utilities.Enums;

namespace Host.Api.Models.Roles
{
    public class UpdateRoleApiRequest : IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public bool Active { get; set; }
        [Required]
        public IEnumerable<PermissionName> Permissions { get; set; }
        [Required]
        public IEnumerable<string> Users { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var roleManager = (IRoleManager)validationContext.GetService(typeof(IRoleManager));
            var userManager = (IUserManager)validationContext.GetService(typeof(IUserManager));
            _ = roleManager.GetRoleByCodeAsync(Code).GetAwaiter().GetResult();
            var invalidUsers = userManager.CheckUsersExistsAsync(Users).GetAwaiter().GetResult();
            foreach (var user in invalidUsers)
            {
                results.Add(new ValidationResult($"Invalid or inactive user: {user}"));
            }
            return results;
        }
    }
}
