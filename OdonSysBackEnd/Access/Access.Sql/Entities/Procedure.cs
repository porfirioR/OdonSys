namespace Access.Sql.Entities
{
    public class Procedure : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        virtual public IEnumerable<ProcedureTooth> ProcedureTeeth { get; set; }
        virtual public IEnumerable<ClientProcedure> ClientProcedures { get; set; }
    }
}
