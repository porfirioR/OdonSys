using Utilities.Enums;

namespace Access.Sql.Entities;

public class Permission : BaseEntity
{
    public PermissionName Name { get; set; }
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; }
}
