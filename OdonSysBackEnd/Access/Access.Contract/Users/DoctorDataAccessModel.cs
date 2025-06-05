using Utilities.Enums;

namespace Access.Contract.Users;

public class DoctorDataAccessModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string Surname { get; set; }
    public string SecondSurname { get; set; }
    public string Document { get; set; }
    public Country Country { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string UserName { get; set; }
    public bool Active { get; set; }
    public bool Approved { get; set; }
    public IEnumerable<string> Roles { get; set; }

    public DoctorDataAccessModel(
        string id,
        string name,
        string middleName,
        string surname,
        string secondSurname,
        string document,
        Country country,
        string email,
        string phone,
        string userName,
        bool active,
        bool approved,
        IEnumerable<string> roles
    )
    {
        Id = id;
        Name = name;
        MiddleName = middleName;
        Surname = surname;
        SecondSurname = secondSurname;
        Document = document;
        Country = country;
        Email = email;
        Phone = phone;
        UserName = userName;
        Active = active;
        Approved = approved;
        Roles = roles;
    }
}
