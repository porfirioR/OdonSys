using Access.Contract.Procedure;
using Access.Sql.Entities;
using AutoMapper;
using System.Linq;

namespace Access.Admin.Mapper
{
    public class ProcedureAccessProfile : Profile
    {
        public ProcedureAccessProfile()
        {
            CreateMap<CreateProcedureAccessRequest, Procedure>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.ProcedureTeeth, opt => opt.Ignore());

            CreateMap<UpdateProcedureAccessRequest, Procedure>()
                .ForMember(dest => dest.ProcedureTeeth, opt => opt.Ignore());

            CreateMap<Procedure, ProcedureAccessModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.ProcedureTeeth, opt => opt.MapFrom(src => src.ProcedureTeeth.Select(x => x.ToothId.ToString())));
        }
    }
}
