using System.Collections.Generic;
using Utilities.Enums;

namespace Access.Sql.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MiddleLastName { get; set; }
        public string Document { get; set; }
        public Country Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual IEnumerable<DoctorRoles> DoctorRoles { get; set; }
        public virtual IEnumerable<DoctorClient> DoctorsClients { get; set; }

        public bool Approved { get; set; }
        public string UserName { get; set; }
        public bool IsDoctor { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
