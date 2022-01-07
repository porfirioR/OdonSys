﻿using System;

namespace Access.Contract.Procedure
{
    public class ProcedureAccessResponse
    {
        public string Id { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EstimatedSessions { get; set; }
    }
}
