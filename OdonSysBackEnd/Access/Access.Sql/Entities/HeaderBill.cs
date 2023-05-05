using System;
using System.Collections.Generic;
using Utilities.Enums;

namespace Access.Sql.Entities
{
    public class HeaderBill : BaseEntity
    {
        public string BillNumber { get; set; }
        public int Iva10 { get; set; }
        public int TotalIva { get; set; }
        public int SubTotal { get; set; }
        public int Total { get; set; }
        public string Timbrado { get; set; }
        public BillStatus Status { get; set; }
        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }
        public virtual IEnumerable<BillDetail> BillDetails { get; set; }
        public virtual IEnumerable<Payment> Payments { get; set; }
    }
}
