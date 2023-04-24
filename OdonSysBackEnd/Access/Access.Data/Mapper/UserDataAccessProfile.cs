using Access.Contract.Auth;
using Access.Contract.Users;
using Access.Sql.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Access.Admin.Mapper
{
    public class UserDataAccessProfile : Profile
    {
        public UserDataAccessProfile()
        {
            CreateMap<UserDataAccessRequest, User>()
                .BeforeMap((src, dest) => src.Email = src.Email ?? dest.Email)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new Guid(src.Id)));

            CreateMap<User, AuthAccessModel>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));

            CreateMap<User, DoctorDataAccessModel>()
                .ForMember(dest => dest.Approved, opt => opt.MapFrom(src => src.Approved));

            CreateMap<User, UserDataAccessModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Approved, opt => opt.MapFrom(src => src.Approved))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Any() ? src.UserRoles.Select(x => x.Role.Code) : new List<string>()));
        }
    }
}
