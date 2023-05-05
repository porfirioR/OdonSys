using System.Collections.Generic;
using Utilities.Enums;

namespace Access.Sql.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public string Document { get; set; }
        public Country Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual IEnumerable<UserRole> UserRoles { get; set; }
        public virtual IEnumerable<UserClient> UserClients { get; set; }
        public virtual IEnumerable<ClientProcedure> UserProcedures { get; set; }
        public virtual IEnumerable<Payment> Payments { get; set; }

        public bool Approved { get; set; }
        public string UserName { get; set; }
        public bool IsDoctor { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
