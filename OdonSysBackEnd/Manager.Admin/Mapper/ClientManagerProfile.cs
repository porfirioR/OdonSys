using Access.Contract.Clients;
using AutoMapper;
using Resources.Contract.Clients;
using System;

namespace Manager.Admin.Mapper
{
    public class ClientManagerProfile : Profile
    {
        public ClientManagerProfile()
        {
            CreateMap<CreateClientRequest, CreateClientAccessRequest>();

            CreateMap<UpdateClientRequest, UpdateClientAccessRequest>();

            CreateMap<ClientAccessResponse, ClientModel>();

            CreateMap<ClientModel, PatchClientAccessRequest>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new Guid(src.Id)));

        }
    }
}
