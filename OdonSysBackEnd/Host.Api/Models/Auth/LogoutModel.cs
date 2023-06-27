namespace Host.Api.Models.Auth
{
    public record LogoutModel(
        bool Susscess,
        string Username
    );
}
