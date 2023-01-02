using Access.Contract.Roles;
using Access.Contract.Users;
using Access.Sql.Entities;
using AutoMapper;
using System.Linq;

namespace Access.Data.Mapper
{
    public class RoleAccessProfile : Profile
    {
        public RoleAccessProfile()
        {
            CreateMap<CreateRoleAccessRequest, Role>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.RolePermissions, opt => opt.MapFrom(src => src.Permissions.ToList().Select(x => new Permission { Name = x })));

            CreateMap<Role, RoleAccessModel>();

            CreateMap<Permission, PermissionAccessModel>();

            CreateMap<UserRole, DoctorDataAccessModel>();

            CreateMap<UpdateRoleAccessRequest, Role>()
                .ForMember(dest => dest.Code, opt => opt.Ignore())
                .ForMember(dest => dest.RolePermissions, opt => opt.MapFrom(src => src.Permissions.ToList().Select(x => new Permission { Name = x })));

        }
    }
}
