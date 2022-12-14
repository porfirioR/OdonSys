using Contract.Admin.Users;
using System.Collections.Generic;

namespace Contract.Admin.Roles
{
    public class RoleModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IEnumerable<string> RolePermissions { get; set; }
        public IEnumerable<DoctorModel> RoleDoctors { get; set; }
    }
}
