namespace Access.Sql.Entities
{
    public class BillDetail : BaseEntity
    {
        public Guid HeaderBillId { get; set; }
        public Guid ClientProcedureId { get; set; }
        public int ProcedurePrice { get; set; }
        public int FinalPrice { get; set; }

        public virtual HeaderBill HeaderBill { get; set; }
        public virtual ClientProcedure ClientProcedure { get; set; }
    }
}
