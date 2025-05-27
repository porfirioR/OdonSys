namespace Host.Api.Contract.Authorization;

public record LogoutModel(
    bool Susscess,
    string Username
);
