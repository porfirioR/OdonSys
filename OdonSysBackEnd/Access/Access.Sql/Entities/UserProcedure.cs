using System;
using System.Collections.Generic;

namespace Access.Sql.Entities
{
    public class UserProcedure : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ProcedureId { get; set; }
        public int Price { get; set; }
        public virtual IEnumerable<User> Users { get; set; }
        public virtual IEnumerable<Procedure> Procedures { get; set; }
    }
}
