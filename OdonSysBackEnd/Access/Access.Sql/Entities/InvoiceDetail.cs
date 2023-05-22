namespace Access.Sql.Entities
{
    public class InvoiceDetail : BaseEntity
    {
        public Guid InvoiceId { get; set; }
        public Guid ClientProcedureId { get; set; }
        public int ProcedurePrice { get; set; }
        public int FinalPrice { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual ClientProcedure ClientProcedure { get; set; }
    }
}
