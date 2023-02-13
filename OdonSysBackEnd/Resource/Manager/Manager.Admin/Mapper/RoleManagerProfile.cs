using Access.Contract.Roles;
using AutoMapper;
using Contract.Admin.Roles;
using System.Linq;

namespace Manager.Admin.Mapper
{
    internal class RoleManagerProfile : Profile
    {
        public RoleManagerProfile()
        {
            CreateMap<CreateRoleRequest, CreateRoleAccessRequest>();

            CreateMap<UpdateRoleRequest, UpdateRoleAccessRequest>()
                .ForMember(dest => dest.Active, opt => opt.Ignore());

            CreateMap<RoleAccessModel, RoleModel>();
        }
    }
}
