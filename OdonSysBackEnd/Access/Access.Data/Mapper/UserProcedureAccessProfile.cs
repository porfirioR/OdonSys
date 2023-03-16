using Access.Contract.Procedure;
using Access.Sql.Entities;
using AutoMapper;
using System;
using System.Linq;

namespace Access.Admin.Mapper
{
    public class UserProcedureAccessProfile : Profile
    {
        public UserProcedureAccessProfile()
        {
            CreateMap<UpsertUserProcedureAccessRequest, UserProcedure>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => new Guid(src.UserId)))
                .ForMember(dest => dest.ProcedureId, opt => opt.MapFrom(src => new Guid(src.ProcedureId)));

            CreateMap<UpdateProcedureAccessRequest, Procedure>()
                .ForMember(dest => dest.ProcedureTeeth, opt => opt.Ignore());

            CreateMap<Procedure, ProcedureAccessModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.ProcedureTeeth, opt => opt.MapFrom(src => src.ProcedureTeeth.Select(x => x.ToothId.ToString())));
        }
    }
}
