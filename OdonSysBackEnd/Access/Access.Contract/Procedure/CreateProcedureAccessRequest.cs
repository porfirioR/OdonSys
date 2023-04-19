using System.Collections.Generic;

namespace Access.Contract.Procedure
{
    public class CreateProcedureAccessRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> ProcedureTeeth { get; set; }
    }
}
