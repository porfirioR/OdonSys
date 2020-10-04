using System.ComponentModel.DataAnnotations;

namespace OdonSys.Storage.Sql
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
    }
}
