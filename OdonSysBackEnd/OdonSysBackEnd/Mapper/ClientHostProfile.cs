using AutoMapper;
using OdonSysBackEnd.Models.Clients;
using Resources.Contract.Clients;

namespace OdonSysBackEnd.Mapper
{
    public class ClientHostProfile : Profile
    {
        public ClientHostProfile()
        {
            CreateMap<CreateClientApiRequest, CreateClientRequest>();
            CreateMap<UpdateClientApiRequest, UpdateClientRequest>();

        }
    }
}
