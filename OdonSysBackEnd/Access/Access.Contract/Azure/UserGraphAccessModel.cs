using Utilities.Enums;

namespace Access.Contract.Azure
{
    public record UserGraphAccessModel
    (
        string Id,
        string Username,
        string Email,
        string Name,
        string Surname,
        string Document,
        string Phone,
        Country Country,
        string MiddleName,
        string SecondSurname,
        IEnumerable<string> Roles
    );
}
