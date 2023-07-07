using Access.Contract.ClientProcedures;
using Access.Sql.Entities;

namespace Access.Data.Mapper
{
    internal class ClientProcedureDataAccessBuilder : IClientProcedureDataAccessBuilder
    {
        public ClientProcedure MapCreateClientProcedureAccessRequestToClientProcedure(CreateClientProcedureAccessRequest request)
        {
            var client = new ClientProcedure()
            {
                UserClientId = new Guid(request.UserClientId),
                ProcedureId = new Guid(request.ProcedureId),
                Active = true
            };
            return client;
        }

        public ClientProcedure MapUpdateClientProcedureAccessRequestToClientProcedure(UpdateClientProcedureAccessRequest request, ClientProcedure entity)
        {
            entity.ProcedureId = new Guid(request.ProcedureId);
            entity.UserClientId = new Guid(request.UserClientId);
            return entity;
        }

        public ClientProcedureAccessModel MapClientProcedureToClientProcedureAccessModel(ClientProcedure entity) => new(
            entity.Id.ToString(),
            entity.ProcedureId.ToString(),
            entity.UserClientId.ToString()
        );
    }
}
