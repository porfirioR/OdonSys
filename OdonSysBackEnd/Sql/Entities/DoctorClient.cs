using System;

namespace Sql.Entities
{
    public class DoctorClient : BaseEntity
    {
        public Guid ClientId { get; set; }
        public Guid DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Client Client { get; set; }
    }
}
