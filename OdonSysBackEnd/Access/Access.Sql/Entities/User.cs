using System;

namespace Access.Sql.Entities
{
    public class User
    {
        public Guid DoctorId { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
