using Utilities.Enums;

namespace Access.Contract.Users;

public record UserDataAccessRequest
(
    string Id,
    string Name,
    string MiddleName,
    string Surname,
    string SecondSurname,
    string Document,
    string Password,
    string Phone,
    string Email,
    Country Country,
    bool Active
)
{
    public string ExternalUserId { get; set; }
    public string UserName { get; set; }
};
