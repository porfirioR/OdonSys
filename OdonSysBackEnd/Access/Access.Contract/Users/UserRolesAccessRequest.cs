using System.Collections.Generic;

namespace Access.Contract.Users
{
    public record UserRolesAccessRequest(
        string UserId,
        IEnumerable<string> Roles
    );
}
