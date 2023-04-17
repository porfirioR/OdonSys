using Access.Contract.Users;
using System;
using System.Collections.Generic;

namespace Access.Contract.Roles
{
    public class RoleAccessModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IEnumerable<string> RolePermissions { get; set; }
        public IEnumerable<DoctorDataAccessModel> UserRoles { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
    }
}
