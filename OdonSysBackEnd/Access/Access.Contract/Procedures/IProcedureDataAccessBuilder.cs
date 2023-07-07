using Access.Sql.Entities;

namespace Access.Contract.Procedures
{
    public interface IProcedureDataAccessBuilder
    {
        Procedure MapCreateProcedureAccessRequestToProcedure(CreateProcedureAccessRequest createProcedureAccessRequest);
        Procedure MapUpdateProcedureAccessRequestToProcedure(UpdateProcedureAccessRequest updateProcedureAccessRequest, Procedure procedure);
        ProcedureAccessModel MapProcedureToProcedureAccessModel(Procedure procedure);
    }
}
