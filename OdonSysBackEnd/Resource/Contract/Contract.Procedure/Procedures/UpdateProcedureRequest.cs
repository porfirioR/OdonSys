using System.Collections.Generic;

namespace Contract.Procedure.Procedures
{
    public class UpdateProcedureRequest
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> ProcedureTeeth { get; set; }
    }
}
