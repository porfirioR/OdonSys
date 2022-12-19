using System;

namespace Access.Sql.Entities
{
    public class DoctorClient : BaseEntity
    {
        public Guid ClientId { get; set; }
        public Guid DoctorId { get; set; }
        public virtual User Doctor { get; set; }
        public virtual Client Client { get; set; }
    }
}
