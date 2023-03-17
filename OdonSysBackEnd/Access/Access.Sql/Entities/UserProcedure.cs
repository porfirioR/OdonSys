using System;
using System.Collections.Generic;

namespace Access.Sql.Entities
{
    public class UserProcedure : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ProcedureId { get; set; }
        public int Price { get; set; }
        public virtual User User { get; set; }
        public virtual Procedure Procedure { get; set; }
    }
}
