using Utilities.Enums;

namespace Contract.Administration.Clients
{
    public record CreateClientRequest
    (
        string Name,
        string MiddleName,
        string Surname,
        string SecondSurname,
        string Document,
        string Ruc,
        Country Country,
        string Phone,
        string Email
    )
    {
        public string UserId { get; set; }
    }
}
