using System;

namespace Access.Sql.Entities
{
    public class DoctorRoles
    {
        public Guid DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
