using Utilities.Enums;

namespace Sql.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Document { get; set; }
        public string Ruc { get; set; }
        public Country Country { get; set; }
    }
}
