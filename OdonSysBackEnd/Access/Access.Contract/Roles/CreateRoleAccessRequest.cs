using System.Collections.Generic;
using Utilities.Enums;

namespace Access.Contract.Roles
{
    public class CreateRoleAccessRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IEnumerable<PermissionName> Permissions { get; set; }
    }
}
