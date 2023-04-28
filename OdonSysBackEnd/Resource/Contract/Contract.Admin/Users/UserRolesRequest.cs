using System.Collections.Generic;

namespace Contract.Admin.Users
{
    public record UserRolesRequest(
        string UserId,
        IEnumerable<string> Roles
    );
}
