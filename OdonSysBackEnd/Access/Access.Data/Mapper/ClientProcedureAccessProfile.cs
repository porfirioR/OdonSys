using Access.Contract.ClientProcedure;
using Access.Sql.Entities;
using AutoMapper;

namespace Access.Data.Mapper
{
    public class ClientProcedureAccessProfile : Profile
    {
        public ClientProcedureAccessProfile()
        {

            CreateMap<CreateClientProcedureAccessRequest, ClientProcedure>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.UserClientId, opt => opt.MapFrom(src => new Guid(src.UserClientId)))
                .ForMember(dest => dest.ProcedureId, opt => opt.MapFrom(src => new Guid(src.ProcedureId)));

            CreateMap<UpdateClientProcedureAccessRequest, ClientProcedure>()
                .ForMember(dest => dest.UserClientId, opt => opt.MapFrom(src => new Guid(src.UserClientId)))
                .ForMember(dest => dest.ProcedureId, opt => opt.MapFrom(src => new Guid(src.ProcedureId)));
        }
    }
}
