using AutoMapper;
using Contract.Admin.Clients;
using Host.Api.Models.Clients;

namespace Host.Api.Mapper
{
    public class ClientHostProfile : Profile
    {
        public ClientHostProfile()
        {
            CreateMap<CreateClientApiRequest, CreateClientRequest>();

            CreateMap<UpdateClientApiRequest, UpdateClientRequest>()
                .ForMember(dest => dest.Active, opt => opt.Ignore());

            CreateMap<ClientModel, UpdateClientRequest>();

            CreateMap<AssignClientApiRequest, AssignClientRequest>();

        }
    }
}
