using Access.Contract.Auth;
using Access.Contract.Users;
using Access.Sql.Entities;
using AutoMapper;
using Contract.Workspace.User;
using System;

namespace Access.Admin.Mapper
{
    public class UserDataAccessProfile : Profile
    {
        public UserDataAccessProfile()
        {
            CreateMap<RegisterUserRequest, UserDataAccessRequest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UserDataAccessRequest, Doctor>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new Guid(src.Id)));

            CreateMap<User, AuthAccessModel>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));

            CreateMap<Doctor, UserDataAccessModel>()
                .ForMember(dest => dest.Approved, opt => opt.MapFrom(src => src.User.Approved))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        }
    }
}
