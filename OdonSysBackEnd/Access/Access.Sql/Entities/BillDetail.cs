using System;
using System.Collections.Generic;

namespace Access.Sql.Entities
{
    public class BillDetail : BaseEntity
    {
        public Guid HeaderBillId { get; set; }
        public Guid ClientProcedureId { get; set; }
        public int FinalPrice { get; set; }

        public virtual HeaderBill HeaderBill { get; set; }
        public virtual ClientProcedure ClientProcedure { get; set; }
        public virtual IEnumerable<Payment> Payments { get; set; }
    }
}
