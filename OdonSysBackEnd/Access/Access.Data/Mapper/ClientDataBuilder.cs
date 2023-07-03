using Access.Contract.Clients;
using Access.Contract.Users;
using Access.Sql.Entities;

namespace Access.Data.Mapper
{
    internal class ClientDataBuilder : IClientDataBuilder
    {
        public UserClient MapAssignClientAccessRequestToUserClient(AssignClientAccessRequest assignClientAccessRequest)
        {
            var client = new UserClient
            {
                Id = Guid.NewGuid(),
                ClientId = new Guid(assignClientAccessRequest.ClientId),
                UserId = new Guid(assignClientAccessRequest.UserId),
                Active = true
            };
            return client;
        }

        public ClientAccessModel MapClientToClientAccessModel(Client client)
        {
            var clientAccessModel = new ClientAccessModel(
                client.Id,
                client.Active,
                client.DateCreated,
                client.DateModified,

            );
            return clientAccessModel;
        }

        public Client MapCreateClientAccessRequestToClient(CreateClientAccessRequest createClientAccessRequest)
        {
            var client = new Client()
            {
                Name = createClientAccessRequest.Name,
                MiddleName = createClientAccessRequest.MiddleName,
                Surname = createClientAccessRequest.Surname,
                SecondSurname = createClientAccessRequest.SecondSurname,
                Document = createClientAccessRequest.Document,
                Ruc = createClientAccessRequest.Ruc,
                Phone = createClientAccessRequest.Phone,
                Country = createClientAccessRequest.Country,
                Debts = createClientAccessRequest.Debts,
                Email = createClientAccessRequest.Email,
                Active = true
            };
            return client;
        }

        public Client MapUpdateClientAccessRequestToClient(UpdateClientAccessRequest updateClientAccessRequest, Client client = null)
        {
            client ??= new Client();
            client.Id = new Guid(updateClientAccessRequest.Id);
            client.Active = updateClientAccessRequest.Active;
            client.Name = updateClientAccessRequest.Name;
            client.MiddleName = updateClientAccessRequest.MiddleName;
            client.Surname = updateClientAccessRequest.Surname;
            client.SecondSurname = updateClientAccessRequest.SecondSurname;
            client.Phone = updateClientAccessRequest.Phone;
            client.Email = updateClientAccessRequest.Email;
            return client;
        }

        public UserClient MapUserClientAccessRequestToUserClient(UserClientAccessRequest userClientAccessRequest)
        {
            var userClient = new UserClient()
            {
                Id = Guid.NewGuid(),
                ClientId = new Guid(userClientAccessRequest.ClientId),
                UserId = new Guid(userClientAccessRequest.UserId)
            };
            return userClient;
        }
    }
}
