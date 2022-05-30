using System;

namespace Access.Sql.Entities
{
    public class RolePermission
    {
        public Guid PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
        public Guid RolId { get; set; }
        public virtual Role Role { get; set; }
    }
}
