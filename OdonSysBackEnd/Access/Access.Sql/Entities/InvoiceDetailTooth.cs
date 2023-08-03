namespace Access.Sql.Entities
{
    public class InvoiceDetailTooth : BaseEntity
    {
        public Guid ToothId { get; set; }
        public Guid InvoiceDetailId { get; set; }

        public virtual Tooth Tooth { get; set; }
        public virtual InvoiceDetail InvoiceDetail { get; set; }
    }
}
