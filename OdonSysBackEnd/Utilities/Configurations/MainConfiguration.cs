namespace Utilities.Configurations
{
    public class MainConfiguration
    {
        public CloudinarySettings Cloudinary { get; set; }
        public DataAccessSettings ConnectionStrings { get; set; }
        public AuthenticationSettings Authentication { get; set; }
        public SystemSettings SystemSettings { get; set; }
    }
}