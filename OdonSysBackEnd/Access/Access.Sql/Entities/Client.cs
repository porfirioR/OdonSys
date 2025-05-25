using Utilities.Enums;

namespace Access.Sql.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public string Document { get; set; }
        public string Ruc { get; set; }
        public string Phone { get; set; }
        public Country Country { get; set; }
        public bool Debts { get; set; }
        public string Email { get; set; }

        public virtual IEnumerable<UserClient> UserClients { get; set; }
        public virtual IEnumerable<Invoice> Invoices { get; set; }
        public virtual IEnumerable<Orthodontic> Orthodontics { get; set; }
    }
}
