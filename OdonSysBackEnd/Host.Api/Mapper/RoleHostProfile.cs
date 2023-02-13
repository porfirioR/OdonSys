using AutoMapper;
using Contract.Admin.Roles;
using Host.Api.Models.Role;

namespace Host.Api.Mapper
{
    public class RoleHostProfile : Profile
    {
        public RoleHostProfile()
        {
            CreateMap<CreateRoleApiRequest, CreateRoleRequest>();

            CreateMap<UpdateRoleApiRequest, UpdateRoleRequest>();

        }
    }
}
