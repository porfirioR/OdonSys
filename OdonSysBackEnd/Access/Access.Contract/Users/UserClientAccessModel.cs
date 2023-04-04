using System;

namespace Access.Contract.Users
{
    public record UserClientAccessModel(
        Guid Id,
        Guid ClientId,
        Guid UserId
    ) { }
}
