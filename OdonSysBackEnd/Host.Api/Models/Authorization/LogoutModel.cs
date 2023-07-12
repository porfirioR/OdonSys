namespace Host.Api.Models.Authorization
{
    public record LogoutModel(
        bool Susscess,
        string Username
    );
}
