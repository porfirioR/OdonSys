﻿using Access.Contract;
using AutoMapper;
using Contract.Workspace.Teeth;

namespace Manager.Workspace.Teeth
{
    public class ToothManagerProfile : Profile
    {
        public ToothManagerProfile()
        {
            CreateMap<ToothAccessResponse, ToothModel>();
        }
    }
}