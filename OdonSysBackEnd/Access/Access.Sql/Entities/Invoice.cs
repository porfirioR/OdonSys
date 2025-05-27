using Utilities.Enums;

namespace Access.Sql.Entities;

public class Invoice : BaseEntity
{
    public string InvoiceNumber { get; set; }
    public int Iva10 { get; set; }
    public int TotalIva { get; set; }
    public int SubTotal { get; set; }
    public int Total { get; set; }
    public string Timbrado { get; set; }
    public InvoiceStatus Status { get; set; }
    public Guid ClientId { get; set; }

    public virtual Client Client { get; set; }
    public virtual IEnumerable<InvoiceDetail> InvoiceDetails { get; set; }
    public virtual IEnumerable<Payment> Payments { get; set; }
}
