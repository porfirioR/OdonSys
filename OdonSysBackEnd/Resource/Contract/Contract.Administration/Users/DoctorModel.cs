using Utilities.Enums;

namespace Contract.Administration.Users;

public record DoctorModel(
    string Id,
    string Name,
    string MiddleName,
    string Surname,
    string SecondSurname,
    string Document,
    Country Country,
    string Email,
    string Phone,
    string UserName,
    bool Active,
    bool Approved,
    IEnumerable<string> Roles
)
{
    public string ExternalUserId { get; set; }
};
