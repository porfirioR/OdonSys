using Access.Contract.Users;
using AutoMapper;
using Sql.Entities;
using System;

namespace Access.Admin.Mapper
{
    public class UserDataAccessProfile : Profile
    {
        public UserDataAccessProfile()
        {
            CreateMap<UserDataAccessRequest, Doctor>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => new Guid(src.Id)));

            CreateMap<Doctor, UserDataAccessModel>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        }
    }
}
