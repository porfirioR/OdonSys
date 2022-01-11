namespace Access.Contract.Procedure
{
    public class CreateProcedureAccessRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string EstimatedSessions { get; set; }
    }
}
