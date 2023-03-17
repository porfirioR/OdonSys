﻿using Access.Contract.Procedure;
using Access.Sql.Entities;
using AutoMapper;
using System;

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
        }
    }
}
