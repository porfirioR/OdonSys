namespace Access.Contract.Procedures
{
    public class CreateProcedureAccessRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> ProcedureTeeth { get; set; }
        public bool XRays { get; set; }
        public int Price { get; set; }
    }
}
