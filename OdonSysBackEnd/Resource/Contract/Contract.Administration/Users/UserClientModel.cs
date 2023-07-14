namespace Contract.Administration.Users
{
    public record UserClientModel(
        Guid Id,
        Guid UserId,
        Guid ClientId
    );
}
