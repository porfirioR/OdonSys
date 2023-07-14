using Contract.Administration.Clients;
using Host.Api.Contract.Clients;

namespace Host.Api.Contract.MapBuilders
{
    public interface IClientHostBuilder
    {
        CreateClientRequest MapCreateClientApiRequestToCreateClientRequest(CreateClientApiRequest createClientApiRequest);
        UpdateClientRequest MapUpdateClientApiRequestToUpdateClientRequest(UpdateClientApiRequest updateClientApiRequest, bool canFullEdit);
        UpdateClientRequest MapClientModelToUpdateClientRequest(ClientModel clientModel);
        AssignClientRequest MapAssignClientApiRequestToAssignClientRequest(AssignClientApiRequest assignClientApiRequest);
    }
}
