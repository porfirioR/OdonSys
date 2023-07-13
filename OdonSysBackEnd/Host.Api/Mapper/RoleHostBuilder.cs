using Contract.Administration.Roles;
using Host.Api.Contract.MapBuilders;
using Host.Api.Contract.Roles;

namespace Host.Api.Mapper
{
    internal sealed class RoleHostBuilder : IRoleHostBuilder
    {
        public CreateRoleRequest MapCreateRoleApiRequestToCreateRoleRequest(CreateRoleApiRequest createRoleApiRequest) => new(
            createRoleApiRequest.Name,
            createRoleApiRequest.Code,
            createRoleApiRequest.Permissions
        );

        public UpdateRoleRequest MapUpdateRoleApiRequestToUpdateRoleRequest(UpdateRoleApiRequest updateRoleApiRequest) => new(
            updateRoleApiRequest.Name,
            updateRoleApiRequest.Code,
            updateRoleApiRequest.Active,
            updateRoleApiRequest.Permissions
        );
    }
}
