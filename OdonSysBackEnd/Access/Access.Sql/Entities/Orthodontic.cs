namespace Access.Sql.Entities;

public class Orthodontic : BaseEntity
{
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public Guid ClientId { get; set; }

    public virtual Client Client { get; set; }
}
