using Utilities.Enums;

namespace Contract.Administration.Users;

public record RegisterUserRequest
(
    string Name,
    string MiddleName,
    string Surname,
    string SecondSurname,
    string Document,
    string Password,
    string Phone,
    string Email,
    Country Country
);
