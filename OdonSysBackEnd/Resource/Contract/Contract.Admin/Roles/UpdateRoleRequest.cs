using Utilities.Enums;

namespace Contract.Admin.Roles
{
    public class UpdateRoleRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public IEnumerable<PermissionName> Permissions { get; set; }
    }
}
