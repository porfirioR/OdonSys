namespace Contract.Administration.Clients
{
    public record AssignClientRequest(
        string UserId,
        string ClientId,
        string ExternalUserId = null
    );
}
