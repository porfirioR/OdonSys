using System.Collections.Generic;
using Utilities.Enums;

namespace Sql.Entities
{
    public class HeaderBill : BaseEntity
    {
        public int BillNumber { get; set; }
        public int ClientId { get; set; }
        public int Iva10 { get; set; }
        public int TotalIva { get; set; }
        public int SubTotal { get; set; }
        public int Total { get; set; }
        public string Timbrado { get; set; }
        public BillStatus Status { get; set; }
        public virtual IEnumerable<PaymentDetails> PaymentDetails { get; set; }
    }
}
