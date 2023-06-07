using System.Diagnostics.CodeAnalysis;

namespace Utilities.Configurations
{
    [ExcludeFromCodeCoverage]
    public class CloudinarySettings
    {
        public const string ConfigSection = "Cloudinary";
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
}
