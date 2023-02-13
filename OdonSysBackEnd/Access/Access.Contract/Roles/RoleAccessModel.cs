using Access.Contract.Users;
using System.Collections.Generic;

namespace Access.Contract.Roles
{
    public class RoleAccessModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IEnumerable<string> RolePermissions { get; set; }
        public IEnumerable<DoctorDataAccessModel> UserRoles { get; set; }
    }
}
