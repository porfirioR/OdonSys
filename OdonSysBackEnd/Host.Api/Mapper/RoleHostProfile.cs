using AutoMapper;
using Contract.Administration.Roles;
using Host.Api.Contract.Roles;

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
