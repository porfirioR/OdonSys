using Access.Contract.Clients;
using AutoMapper;
using Contract.Admin.Clients;
using System;

namespace Manager.Admin.Mapper
{
    public class ClientManagerProfile : Profile
    {
        public ClientManagerProfile()
        {
            CreateMap<CreateClientRequest, CreateClientAccessRequest>();

            CreateMap<UpdateClientRequest, UpdateClientAccessRequest>()
                .ForMember(dest => dest.Active, opt => opt.Ignore());

            CreateMap<ClientAccessModel, ClientModel>();

        }
    }
}
