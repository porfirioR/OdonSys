using Contract.Administration.Clients;
using Host.Api.Contract.Clients;
using Host.Api.Contract.MapBuilders;

namespace Host.Api.Mapper
{
    internal sealed class ClientHostBuilder : IClientHostBuilder
    {
        public CreateClientRequest MapCreateClientApiRequestToCreateClientRequest(CreateClientApiRequest createClientApiRequest) => new(
            createClientApiRequest.Name,
            createClientApiRequest.MiddleName,
            createClientApiRequest.Surname,
            createClientApiRequest.SecondSurname,
            createClientApiRequest.Document,
            createClientApiRequest.Ruc,
            createClientApiRequest.Country,
            createClientApiRequest.Phone,
            createClientApiRequest.Email
        );

        public UpdateClientRequest MapUpdateClientApiRequestToUpdateClientRequest(UpdateClientApiRequest updateClientApiRequest) => new(
            updateClientApiRequest.Id,
            updateClientApiRequest.Name,
            updateClientApiRequest.MiddleName,
            updateClientApiRequest.Surname,
            updateClientApiRequest.SecondSurname,
            updateClientApiRequest.Phone,
            updateClientApiRequest.Email,
            updateClientApiRequest.Active
        );

        public UpdateClientRequest MapClientModelToUpdateClientRequest(ClientModel clientModel) => new(
            clientModel.Id,
            clientModel.Name,
            clientModel.MiddleName,
            clientModel.Surname,
            clientModel.SecondSurname,
            clientModel.Phone,
            clientModel.Email,
            clientModel.Active
        );

        public AssignClientRequest MapAssignClientApiRequestToAssignClientRequest(AssignClientApiRequest assignClientApiRequest) => new(
            assignClientApiRequest.UserId,
            assignClientApiRequest.ClientId
        );
    }
}
