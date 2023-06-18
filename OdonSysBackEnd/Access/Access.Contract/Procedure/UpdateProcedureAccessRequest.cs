namespace Access.Contract.Procedure
{
    public class UpdateProcedureAccessRequest
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int Price { get; set; }
        public IEnumerable<string> ProcedureTeeth { get; set; }
        public bool XRay { get; set; }
    }
}
