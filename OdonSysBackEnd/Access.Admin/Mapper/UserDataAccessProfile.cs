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
            CreateMap<UserDataAccessRequest, Doctor>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => new Guid(src.Id)))
                .ForMember(dest => dest.Document,
                opt => opt.MapFrom(src => src.Document))
                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Country,
                opt => opt.MapFrom(src => src.Country));

            CreateMap<Doctor, UserDataAccessModel>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Document,
                opt => opt.MapFrom(src => src.Document))
                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Country,
                opt => opt.MapFrom(src => src.Country));
        }
    }
}
