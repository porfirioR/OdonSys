using Host.Api.Models.Procedures;

namespace AcceptanceTest.Host.Api.Procedures
{
    internal partial class ProcedureControllerTest
    {
        CreateProcedureApiRequest CreateProcedureApiRequest => new()
        {
            Name = Guid.NewGuid().ToString()[30..],
            Description = Guid.NewGuid().ToString()[30..],
            Price = 0,
            ProcedureTeeth = TeethIds
        };

        UpdateProcedureApiRequest UpdateProcedureApiRequest(string Id) => new()
        {
            Id = Id,
            Description = Guid.NewGuid().ToString()[30..],
            ProcedureTeeth = TeethIds
        };
    }
}
