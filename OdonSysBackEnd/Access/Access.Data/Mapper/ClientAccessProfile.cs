using Access.Contract.Clients;
using Access.Contract.Users;
using Access.Sql.Entities;
using AutoMapper;

namespace Access.Data.Mapper
{
    public class ClientAccessProfile : Profile
    {
        public ClientAccessProfile()
        {
            CreateMap<CreateClientAccessRequest, Client>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true));

            CreateMap<UpdateClientAccessRequest, Client>()
                .ForMember(dest => dest.Debts, opt => opt.Ignore());

            CreateMap<Client, ClientAccessModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Doctors, opt => opt.MapFrom(src => src.UserClients.Select(x => x.User)));

            CreateMap<UserClientAccessRequest, UserClient>();

            CreateMap<AssignClientAccessRequest, UserClient>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => new Guid(src.UserId)))
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => new Guid(src.ClientId)));
        }
    }
}
