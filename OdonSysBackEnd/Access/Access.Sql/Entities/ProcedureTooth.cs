using System;

namespace Access.Sql.Entities
{
    public class ProcedureTooth : BaseEntity
    {
        public Guid ToothId { get; set; }
        public Guid ProcedureId { get; set; }
        public virtual Procedure Procedure { get; set; }
        public virtual Tooth Tooth { get; set; }
    }
}
