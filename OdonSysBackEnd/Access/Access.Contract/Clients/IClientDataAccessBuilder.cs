using Access.Contract.Users;
using Access.Sql.Entities;

namespace Access.Contract.Clients;

public interface IClientDataAccessBuilder
{
    Client MapCreateClientAccessRequestToClient(CreateClientAccessRequest createClientAccessRequest);
    Client MapUpdateClientAccessRequestToClient(UpdateClientAccessRequest updateClientAccessRequest, Client client = null);
    ClientAccessModel MapClientToClientAccessModel(Client client);
    UserClient MapUserClientAccessRequestToUserClient(UserClientAccessRequest userClientAccessRequest);
    UserClient MapAssignClientAccessRequestToUserClient(AssignClientAccessRequest assignClientAccessRequest);
}
