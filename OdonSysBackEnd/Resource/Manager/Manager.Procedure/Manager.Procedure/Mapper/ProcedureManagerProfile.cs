﻿using Access.Contract.Procedure;
using AutoMapper;
using Contract.Procedure.Procedures;

namespace Manager.Procedure.Mapper
{
    public class ProcedureManagerProfile : Profile
    {
        public ProcedureManagerProfile()
        {
            CreateMap<CreateProcedureRequest, CreateProcedureAccessRequest>();

            CreateMap<UpdateProcedureRequest, UpdateProcedureAccessRequest>();

            CreateMap<ProcedureAccessResponse, ProcedureModel>();
        }
    }
}
