using Access.Contract.Procedures;
using Access.Sql.Entities;

namespace Access.Data.Mapper;

internal sealed class ProcedureDataAccessBuilder : IProcedureDataAccessBuilder
{
    public Procedure MapCreateProcedureAccessRequestToProcedure(CreateProcedureAccessRequest createProcedureAccessRequest)
    {
        var procedure = new Procedure()
        {
            Name = createProcedureAccessRequest.Name,
            Description = createProcedureAccessRequest.Description,
            Price = createProcedureAccessRequest.Price,
            XRays = createProcedureAccessRequest.XRays,
            Active = true
        };
        return procedure;
    }

    public ProcedureAccessModel MapProcedureToProcedureAccessModel(Procedure procedure) => new(
        procedure.Id.ToString(),
        procedure.Active,
        procedure.DateCreated,
        procedure.DateModified,
        procedure.Name,
        procedure.Description,
        procedure.Price,
        new List<string>(),
        procedure.XRays
    );

    public Procedure MapUpdateProcedureAccessRequestToProcedure(UpdateProcedureAccessRequest updateProcedureAccessRequest, Procedure procedure)
    {
        procedure.Id = new Guid(updateProcedureAccessRequest.Id);
        procedure.Description = updateProcedureAccessRequest.Description;
        procedure.Active = updateProcedureAccessRequest.Active;
        procedure.Price = updateProcedureAccessRequest.Price;
        procedure.XRays = updateProcedureAccessRequest.XRays;
        return procedure;
    }
}
