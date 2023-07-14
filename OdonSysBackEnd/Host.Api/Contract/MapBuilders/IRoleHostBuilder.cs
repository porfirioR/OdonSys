using Contract.Administration.Roles;
using Host.Api.Contract.Roles;

namespace Host.Api.Contract.MapBuilders
{
    public interface IRoleHostBuilder
    {
        CreateRoleRequest MapCreateRoleApiRequestToCreateRoleRequest(CreateRoleApiRequest createRoleApiRequest);
        UpdateRoleRequest MapUpdateRoleApiRequestToUpdateRoleRequest(UpdateRoleApiRequest updateRoleApiRequest);
    }
}
