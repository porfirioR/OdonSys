namespace Contract.Administration.Users;

public record UserModel
(
    string Id,
    string UserName,
    bool Active,
    bool Approved,
    IEnumerable<string> Roles
);
