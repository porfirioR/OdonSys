using Access.Contract.Users;
using Access.Sql.Entities;
using AutoMapper;
using Contract.Authentication.User;
using System;

namespace Access.Admin.Mapper
{
    public class UserDataAccessProfile : Profile
    {
        public UserDataAccessProfile()
        {
            CreateMap<RegisterUserRequest, UserDataAccessRequest>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UserDataAccessRequest, Doctor>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => new Guid(src.Id)));

            CreateMap<Doctor, UserDataAccessModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        }
    }
}
