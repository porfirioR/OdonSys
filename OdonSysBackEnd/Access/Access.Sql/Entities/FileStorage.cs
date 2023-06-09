namespace Access.Sql.Entities
{
    public class FileStorage : BaseEntity
    {
        public string Url { get; set; }
        public Guid ClientProcedureId { get; set; }

        public virtual ClientProcedure ClientProcedure { get; set; }
    }
}
