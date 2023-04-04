using System;

namespace Contract.Admin.Users
{
    public record UserClientModel(
        Guid Id,
        Guid UserId,
        Guid ClientId
        )
    { }
}
