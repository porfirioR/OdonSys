﻿using System.Collections.Generic;

namespace Access.Sql.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual IEnumerable<Permission> RolePermissions { get; set; }
        public virtual IEnumerable<DoctorRoles> DoctorRoles { get; set; }
    }
}
