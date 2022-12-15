﻿using System;

namespace Access.Sql.Entities
{
    public class Permission : BaseEntity
    {
        public string Code { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
