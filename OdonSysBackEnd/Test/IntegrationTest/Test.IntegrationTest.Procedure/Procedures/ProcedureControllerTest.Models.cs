using Host.Api.Models.Procedures;
using System;

namespace AcceptanceTest.Host.Api.Procedures
{
    internal partial class ProcedureControllerTest
    {
        CreateProcedureApiRequest CreateProcedureApiRequest => new()
        {
            Name = Guid.NewGuid().ToString()[30..],
            Description = "Procedimiento",
            Price = 0,
            ProcedureTeeth = TeethIds
        };

        UpdateProcedureApiRequest UpdateProcedureApiRequest(string Id) => new()
        {
            Id = Id,
            Description = "Procedimiento",
            ProcedureTeeth = TeethIds
        };
    }
}
