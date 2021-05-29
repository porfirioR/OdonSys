using System.Collections.Generic;

namespace Sql.Entities
{
    public class Role: BaseEntity
    {
        public string Name { get; set; }
        public virtual IEnumerable<RolePermission> RolePermissions { get; set; }
        public virtual IEnumerable<DoctorRoles> DoctorRoles { get; set; }
    }
}
