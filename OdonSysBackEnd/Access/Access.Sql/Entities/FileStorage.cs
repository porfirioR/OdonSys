namespace Access.Sql.Entities;

public class FileStorage : BaseEntity
{
    public string FileName { get; set; }
    public string Url { get; set; }
    public string ReferenceId { get; set; }
    public string Format { get; set; }
}
