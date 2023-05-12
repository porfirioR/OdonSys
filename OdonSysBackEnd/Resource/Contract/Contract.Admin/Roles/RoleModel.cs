using Contract.Admin.Users;

namespace Contract.Admin.Roles
{
    public class RoleModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IEnumerable<string> RolePermissions { get; set; }
        public IEnumerable<DoctorModel> UserRoles { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
    }
}
