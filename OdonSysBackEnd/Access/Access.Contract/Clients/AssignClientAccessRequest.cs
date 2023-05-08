namespace Access.Contract.Clients
{
    public record AssignClientAccessRequest(
        string UserId,
        string ClientId
    );
}
