using Utilities.Enums;

namespace Contract.Admin.Roles
{
    public class PermissionModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public PermissionGroup Group { get; set; }
        public PermissionSubGroup SubGroup { get; set; }

        public PermissionModel(string name, string code, PermissionGroup group, PermissionSubGroup subGroup)
        {
            Name = name;
            Code = code;
            Group = group;
            SubGroup = subGroup;
        }
    }
}
