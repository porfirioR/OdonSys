namespace Access.Contract.Users;

public record UserClientAccessRequest(
    string ClientId,
    string UserId
);
