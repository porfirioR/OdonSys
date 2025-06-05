namespace Access.Sql.Entities;

public class UserClient : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ClientId { get; set; }

    public virtual User User { get; set; }
    public virtual Client Client { get; set; }
    public virtual IEnumerable<ClientProcedure> ClientProcedures { get; set; }
}
