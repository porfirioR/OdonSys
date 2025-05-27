using Access.Contract.Clients;

namespace Contract.Administration.Clients;

public interface IClientManagerBuilder
{
    CreateClientAccessRequest MapCreateClientRequestToCreateClientAccessRequest(CreateClientRequest accessRequest);
    UpdateClientAccessRequest MapUpdateClientRequestToUpdateClientAccessRequest(UpdateClientRequest accessRequest);
    ClientModel MapClientAccessModelToClientModel(ClientAccessModel accessModel);
}
