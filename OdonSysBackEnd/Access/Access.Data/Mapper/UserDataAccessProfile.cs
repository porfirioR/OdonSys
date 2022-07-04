using Access.Contract.Auth;
using Access.Contract.Users;
using Access.Sql.Entities;
using AutoMapper;
using System;

namespace Access.Admin.Mapper
{
    public class UserDataAccessProfile : Profile
    {
        public UserDataAccessProfile()
        {
            CreateMap<UserDataAccessRequest, Doctor>()
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new Guid(src.Id)));

            CreateMap<User, AuthAccessModel>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));

            CreateMap<Doctor, DoctorDataAccessModel>()
                .ForMember(dest => dest.Approved, opt => opt.MapFrom(src => src.User.Approved));

            CreateMap<Doctor, UserDataAccessModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Approved, opt => opt.MapFrom(src => src.User.Approved));
        }
    }
}
