using System;
using System.Collections.Generic;

namespace Access.Contract.Procedure
{
    public class ProcedureAccessModel
    {
        public string Id { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EstimatedSessions { get; set; }
        public int Price { get; set; }
        public IEnumerable<string> ProcedureTeeth { get; set; }
    }
}
