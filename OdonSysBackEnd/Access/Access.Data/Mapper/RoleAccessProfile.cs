using Access.Contract.Roles;
using Access.Contract.Users;
using Access.Sql.Entities;
using AutoMapper;
using System;
using System.Linq;

namespace Access.Data.Mapper
{
    public class RoleAccessProfile : Profile
    {
        public RoleAccessProfile()
        {
            CreateMap<CreateRoleAccessRequest, Role>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.RolePermissions, opt => opt.MapFrom(src => src.Permissions.ToList().Select(x => new Permission { Id = Guid.NewGuid(), Name = x, Active = true })));

            CreateMap<Role, RoleAccessModel>()
                .ForMember(dest => dest.RolePermissions, opt => opt.MapFrom(src => src.RolePermissions.Select(x => x.Name)));

            CreateMap<UserRole, DoctorDataAccessModel>();

            CreateMap<UpdateRoleAccessRequest, Role>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Code, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.DateModified, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.RolePermissions, opt => opt.MapFrom(src => src.Permissions.ToList().Select(x => new Permission { Name = x })));

        }
    }
}
