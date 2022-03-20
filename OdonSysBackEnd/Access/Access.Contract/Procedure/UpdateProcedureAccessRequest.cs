using System.Collections.Generic;

namespace Access.Contract.Procedure
{
    public class UpdateProcedureAccessRequest
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> ProcedureTeeth { get; set; }
    }
}
