using Utilities.Enums;

namespace Access.Contract.Clients
{
    public record CreateClientAccessRequest
    (
        string Name,
        string MiddleName,
        string Surname,
        string SecondSurname,
        string Document,
        string Ruc,
        string Phone,
        Country Country,
        bool Debts,
        string Email
    );
}
