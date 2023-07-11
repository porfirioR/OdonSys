namespace Contract.Administration.Clients
{
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
    );
}
