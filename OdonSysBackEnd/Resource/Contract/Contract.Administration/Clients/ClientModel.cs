using Contract.Administration.Users;
using Utilities.Enums;

namespace Contract.Administration.Clients;

public class ClientModel
{
    public string Id { get; set; }
    public bool Active { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string Surname { get; set; }
    public string SecondSurname { get; set; }
    public string Document { get; set; }
    public string Ruc { get; set; }
    public Country Country { get; set; }
    public bool Debts { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public IEnumerable<DoctorModel> Doctors { get; set; }
}
