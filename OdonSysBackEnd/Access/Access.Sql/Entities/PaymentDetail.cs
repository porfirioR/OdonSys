using System;

namespace Access.Sql.Entities
{
    public class PaymentDetail
    {
        public int HeaderBillId { get; set; }
        public int UserId { get; set; }

        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public virtual HeaderBill HeaderBill { get; set; }
        public virtual User User { get; set; }

        public void SetUserName()
        {
            UserName = $"{User.Name} {User.Surname}";
        }
    }
}
