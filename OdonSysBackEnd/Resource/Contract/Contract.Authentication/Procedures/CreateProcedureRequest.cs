namespace Contract.Workspace.Procedures
{
    public class CreateProcedureRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public IEnumerable<string> ProcedureTeeth { get; set; }
    }
}
