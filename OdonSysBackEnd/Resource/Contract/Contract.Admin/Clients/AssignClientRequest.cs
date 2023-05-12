namespace Contract.Admin.Clients
{
    public record AssignClientRequest(
        string UserId,
        string ClientId
    );
}
