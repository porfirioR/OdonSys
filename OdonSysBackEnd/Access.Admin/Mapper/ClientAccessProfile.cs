using Access.Contract.Clients;
using AutoMapper;
using Sql.Entities;

namespace Access.Admin.Mapper
{
    public class ClientAccessProfile : Profile
    {
        public ClientAccessProfile()
        {
            CreateMap<CreateClientAccessRequest, Client>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

            CreateMap<UpdateClientAccessRequest, Client>();

            CreateMap<PatchClientAccessRequest, Client>();

            CreateMap<Client, ClientAccessResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

        }
    }
}
