using Access.Contract.Clients;
using AutoMapper;
using Sql.Entities;

namespace Access.Admin.Mapper
{
    public class ClientAccessProfile : Profile
    {
        public ClientAccessProfile()
        {
            CreateMap<CreateClientAccessRequest, Client>();
            CreateMap<UpdateClientAccessRequest, Client>();
            CreateMap<Client, ClientAccessResponse>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

        }
    }
}
