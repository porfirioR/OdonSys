using Utilities.Enums;

namespace Contract.Administration.Users;

public record UpdateDoctorRequest
(
    string Id,
    string Name,
    string MiddleName,
    string Surname,
    string SecondSurname,
    string Document,
    Country Country,
    string Phone,
    bool Active
);
