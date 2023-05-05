using System;

namespace Access.Sql.Entities
{
    public class Payment : BaseEntity
    {
        public int Amount { get; set; }
        public Guid UserId { get; set; }
        public Guid HeaderBillId { get; set; }
        public virtual User User { get; set; }
        public virtual HeaderBill HeaderBill { get; set; }
    }
}
