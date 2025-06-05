namespace Access.Sql.Entities;

public class Payment : BaseEntity
{
    public int Amount { get; set; }
    public Guid UserId { get; set; }
    public Guid InvoiceId { get; set; }
    public virtual User User { get; set; }
    public virtual Invoice Invoice { get; set; }
}
