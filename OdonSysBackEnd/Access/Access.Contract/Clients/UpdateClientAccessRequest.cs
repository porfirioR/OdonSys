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
    );
}
