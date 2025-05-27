using Utilities.Enums;

namespace Contract.Administration.Clients;

public record UpdateClientRequest
(
    string Id,
    string Name,
    string MiddleName,
    string Surname,
    string SecondSurname,
    string Phone,
    string Email,
    bool Active
)
{
    public Country? Country { get; set; }
    public string Document { get; set; }
};
