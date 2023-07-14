using Utilities.Enums;

namespace Access.Contract.Clients
{
    public record UpdateClientAccessRequest
    (
        string Id,
        bool Active,
        string Name,
        string MiddleName,
        string Surname,
        string SecondSurname,
        string Phone,
        string Email
    )
    {
        public Country? Country { get; set; }
        public string Document { get; set; }
    };
}
