using Utilities.Enums;

namespace Contract.Admin.Roles
{
    public class CreateRoleRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IEnumerable<PermissionName> Permissions { get; set; }
    }
}
