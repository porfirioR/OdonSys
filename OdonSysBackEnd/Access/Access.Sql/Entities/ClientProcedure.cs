using System;
using Utilities.Enums;

namespace Access.Sql.Entities
{
    public class ClientProcedure : BaseEntity
    {
        public Guid UserClientId { get; set; }
        public Guid ProcedureId { get; set; }
        public int Price { get; set; }
        public ProcedureStatus Status { get; set; }

        public virtual UserClient UserClient { get; set; }
        public virtual Procedure Procedure { get; set; }
        public virtual BillDetail BillDetail { get; set; }
    }
}
