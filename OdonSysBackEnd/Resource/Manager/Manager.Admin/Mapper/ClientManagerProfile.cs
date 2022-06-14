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

            CreateMap<UpdateClientRequest, UpdateClientAccessRequest>();

            CreateMap<ClientAccessModel, ClientModel>();

            CreateMap<ClientModel, PatchClientAccessRequest>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new Guid(src.Id)));

        }
    }
}
