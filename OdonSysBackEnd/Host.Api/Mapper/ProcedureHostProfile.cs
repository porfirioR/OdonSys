using AutoMapper;
using Contract.Workspace.Procedures;
using Host.Api.Models.Procedures;

namespace Host.Api.Mapper
{
    public class ProcedureHostProfile : Profile
    {
        public ProcedureHostProfile()
        {
            CreateMap<CreateProcedureApiRequest, CreateProcedureRequest>().ReverseMap();
            CreateMap<UpdateProcedureApiRequest, UpdateProcedureRequest>().ReverseMap();
        }
    }
}
