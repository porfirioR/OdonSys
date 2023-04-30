using Access.Contract.Clients;
using AutoMapper;
using Contract.Admin.Clients;

namespace Manager.Admin.Mapper
{
    public class ClientManagerProfile : Profile
    {
        public ClientManagerProfile()
        {
            CreateMap<CreateClientRequest, CreateClientAccessRequest>();

            CreateMap<UpdateClientRequest, UpdateClientAccessRequest>();

            CreateMap<ClientAccessModel, ClientModel>();

        }
    }
}
