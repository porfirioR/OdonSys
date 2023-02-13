using Access.Contract.Clients;
using Access.Sql.Entities;
using AutoMapper;
using System.Linq;

namespace Access.Admin.Mapper
{
    public class ClientAccessProfile : Profile
    {
        public ClientAccessProfile()
        {
            CreateMap<CreateClientAccessRequest, Client>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

            CreateMap<UpdateClientAccessRequest, Client>()
                .ForMember(dest => dest.Document, opt => opt.Ignore())
                .ForMember(dest => dest.Ruc, opt => opt.Ignore())
                .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ForMember(dest => dest.Debts, opt => opt.Ignore());

            CreateMap<Client, ClientAccessModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.UserClients.Select(x => x.User)));

        }
    }
}
