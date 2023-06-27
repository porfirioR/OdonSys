namespace Contract.Workspace.Procedures
{
    public class UpdateProcedureRequest
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int Price { get; set; }
        public IEnumerable<string> ProcedureTeeth { get; set; }
        public bool XRays { get; set; }
    }
}
