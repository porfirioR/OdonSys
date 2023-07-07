using Access.Sql.Entities;

namespace Access.Contract.ClientProcedures
{
    public interface IClientProcedureDataAccessBuilder
    {
        ClientProcedure MapCreateClientProcedureAccessRequestToClientProcedure(CreateClientProcedureAccessRequest request);
        ClientProcedure MapUpdateClientProcedureAccessRequestToClientProcedure(UpdateClientProcedureAccessRequest request, ClientProcedure entity);
        ClientProcedureAccessModel MapClientProcedureToClientProcedureAccessModel(ClientProcedure entity);
    }
}
