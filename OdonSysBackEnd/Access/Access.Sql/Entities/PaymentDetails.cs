using System;

namespace Access.Sql.Entities
{
    public class PaymentDetails
    {
        public int HeaderBillId { get; set; }
        public int DoctorId { get; set; }

        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string DoctorName { get; set; }
        public virtual HeaderBill HeaderBill { get; set; }
        public virtual Doctor Doctor { get; set; }

        public void SetDoctorName()
        {
            DoctorName = $"{Doctor.Name} {Doctor.LastName}";
        }
    }
}
