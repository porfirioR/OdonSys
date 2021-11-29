namespace Sql.Entities
{
    public class DoctorClient : BaseEntity
    {
        public int ClientId { get; set; }
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Client Client { get; set; }
    }
}
