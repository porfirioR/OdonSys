using System.Collections.Generic;

namespace Contract.Procedure.Procedures
{
    public class CreateProcedureRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string EstimatedSessions { get; set; }
        public IEnumerable<string> ProcedureTeeth { get; set; }
    }
}
