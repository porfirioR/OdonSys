using Access.Contract.Users;
using Utilities.Enums;

namespace Access.Contract.Clients;

public record ClientAccessModel(
    string Id,
    bool Active,
    DateTime DateCreated,
    DateTime DateModified,
    string Name,
    string MiddleName,
    string Surname,
    string SecondSurname,
    string Document,
    string Ruc,
    Country Country,
    bool Debts,
    string Phone,
    string Email,
    string UserCreated,
    IEnumerable<DoctorDataAccessModel> Doctors
);
