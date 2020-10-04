using System;

namespace OdonSys.Storage.Sql.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Active { get; set; }
    }
}
