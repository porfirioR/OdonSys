using Utilities.Enums;

namespace Access.Sql.Entities
{
    public class Tooth : BaseEntity
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public Jaw Jaw { get; set; }
        public Quadrant Quadrant { get; set; }
        public DentalGroup Group { get; set; }
        //public virtual IEnumerable<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
