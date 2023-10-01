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
        string SecondName,
        string SecondLastname
    );
}
