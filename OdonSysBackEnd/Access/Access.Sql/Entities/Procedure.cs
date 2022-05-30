﻿using System.Collections.Generic;

namespace Access.Sql.Entities
{
    public class Procedure : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string EstimatedSessions { get; set; }
        virtual public IEnumerable<ProcedureTooth> ProcedureTeeth { get; set; }
    }
}
