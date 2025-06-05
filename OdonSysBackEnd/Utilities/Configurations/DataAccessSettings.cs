using System.Diagnostics.CodeAnalysis;

namespace Utilities.Configurations;

[ExcludeFromCodeCoverage]
public class DataAccessSettings
{
    public const string ConfigSection = "ConnectionStrings";
    public string DefaultConnection { get; set; }
}
