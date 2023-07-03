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
            var doctorDataAccessModels = client.UserClients.Any() ?
                client.UserClients.Select(x => new DoctorDataAccessModel(
                    x.User.Id.ToString(),
                    x.User.Name,
                    x.User.MiddleName,
                    x.User.Surname,
                    x.User.SecondSurname,
                    x.User.Document,
                    x.User.Country,
                    x.User.Email,
                    x.User.Phone,
                    x.User.UserName,
                    x.User.Active,
                    x.User.Approved,
                    x.User.UserRoles.Any() ? x.User.UserRoles.Select(x => x.Role.Code) : new List<string>()
                )) :
                new List<DoctorDataAccessModel>();

            var clientAccessModel = new ClientAccessModel(
                client.Id.ToString(),
                client.Active,
                client.DateCreated,
                client.DateModified,
                client.Name,
                client.MiddleName,
                client.Surname,
                client.SecondSurname,
                client.Document,
                client.Ruc,
                client.Country,
                client.Debts,
                client.Phone,
                client.Email,
                client.UserCreated,
                doctorDataAccessModels
            );
            return clientAccessModel;
        }

        public Client MapCreateClientAccessRequestToClient(CreateClientAccessRequest createClientAccessRequest) => new()
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

        public UserClient MapUserClientAccessRequestToUserClient(UserClientAccessRequest userClientAccessRequest) => new()
        {
            Id = Guid.NewGuid(),
            ClientId = new Guid(userClientAccessRequest.ClientId),
            UserId = new Guid(userClientAccessRequest.UserId)
        };
    }
}
