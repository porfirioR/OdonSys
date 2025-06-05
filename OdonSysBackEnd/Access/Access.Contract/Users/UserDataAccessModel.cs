namespace Access.Contract.Users;

public record UserDataAccessModel(
    string Id,
    string UserName,
    bool Active,
    bool Approved
)
{
    public IEnumerable<string> Roles { get; set; }
};