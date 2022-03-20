using Access.Sql;
using System.Collections.Generic;
using Utilities.Enums;

namespace Sql.Entities
{
    public class Tooth : BaseEntity
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public Jaw Jaw { get; set; }
        public Quadrant Quadrant { get; set; }
        public DentalGroup Group { get; set; }
        virtual public IEnumerable<ProcedureTooth> ProcedureTeeth { get; set; }
    }
}
