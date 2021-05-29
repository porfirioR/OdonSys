using System;

namespace Sql.Entities
{
    public class DoctorRoles : BaseEntity
    {
        public Guid DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        public Guid RolId { get; set; }
        public virtual Role Role { get; set; }
    }
}
