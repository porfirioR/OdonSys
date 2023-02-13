using System.Collections.Generic;
using Utilities.Enums;

namespace Access.Contract.Roles
{
    public class UpdateRoleAccessRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public IEnumerable<PermissionName> Permissions { get; set; }
    }
}
