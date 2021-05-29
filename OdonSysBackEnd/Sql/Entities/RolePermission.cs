﻿using System;

namespace Sql.Entities
{
    public class RolePermission : BaseEntity
    {
        public Guid PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
        public Guid RolId { get; set; }
        public Role Role { get; set; }
    }
}
