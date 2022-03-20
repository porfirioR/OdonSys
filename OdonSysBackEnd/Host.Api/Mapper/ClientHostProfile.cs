﻿using AutoMapper;
using Contract.Admin.Clients;
using Host.Api.Models.Clients;

namespace Host.Api.Mapper
{
    public class ClientHostProfile : Profile
    {
        public ClientHostProfile()
        {
            CreateMap<CreateClientApiRequest, CreateClientRequest>();

            CreateMap<UpdateClientApiRequest, UpdateClientRequest>();

            CreateMap<AssignClientApiRequest, AssignClientRequest>();

        }
    }
}