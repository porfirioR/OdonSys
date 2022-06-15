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

            CreateMap<UpdateClientAccessRequest, Client>();

            CreateMap<PatchClientAccessRequest, Client>();

            CreateMap<Client, ClientAccessModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Doctors, opt => opt.MapFrom(src => src.DoctorsClients.Select(x => x.Doctor)));

        }
    }
}
