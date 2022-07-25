using System.Collections.Generic;

namespace Access.Contract.Roles
{
    public class UpdateRoleAccessRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
