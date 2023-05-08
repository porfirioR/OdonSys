namespace Contract.Admin.Users
{
    public record UserRolesRequest(
        string UserId,
        IEnumerable<string> Roles
    );
}
