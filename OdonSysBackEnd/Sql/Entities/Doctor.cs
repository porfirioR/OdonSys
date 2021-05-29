using System.Collections.Generic;
using Utilities.Enums;

namespace Sql.Entities
{
    public class Doctor : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public Country Country { get; set; }
        public virtual IEnumerable<DoctorRoles> DoctorRoles { get; set; }
    }
}
