namespace Contract.Administration.Users;

public record UserRolesRequest(
    string UserId,
    IEnumerable<string> Roles
);
