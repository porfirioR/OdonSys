using System.Collections.Generic;

namespace Contract.Admin.Roles
{
    public class CreateRoleRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
