using System.ComponentModel.DataAnnotations;
using Utilities.Enums;

namespace Host.Api.Contract.Roles;

public class CreateRoleApiRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public IEnumerable<PermissionName> Permissions { get; set; }
}
