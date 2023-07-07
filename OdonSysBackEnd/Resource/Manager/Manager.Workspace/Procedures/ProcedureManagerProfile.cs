using Access.Contract.Procedures;
using AutoMapper;
using Contract.Workspace.Procedures;

namespace Manager.Workspace.Procedures
{
    public class ProcedureManagerProfile : Profile
    {
        public ProcedureManagerProfile()
        {
            CreateMap<CreateProcedureRequest, CreateProcedureAccessRequest>();

            CreateMap<UpdateProcedureRequest, UpdateProcedureAccessRequest>();

            CreateMap<ProcedureAccessModel, ProcedureModel>();

            CreateMap<ProcedureModel, UpdateProcedureRequest>();
        }
    }
}
