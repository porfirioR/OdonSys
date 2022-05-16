using Access.Sql;
using System.Collections.Generic;

namespace Access.Sql.Entities
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }
        public virtual IEnumerable<RolePermission> RolePermissions { get; set; }
    }
}
