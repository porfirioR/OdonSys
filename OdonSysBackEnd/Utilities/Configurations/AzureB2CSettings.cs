namespace Utilities.Configurations
{
    public class AzureB2CSettings
    {
        public const string ConfigSection = "AzureB2C";
        public string ClientSecret { get; set; }
        public string Domain { get; set; }
        public string EnvironmentBackEnd { get; set; }
        public string EnvironmentFrontEnd { get; set; }
        public string FrontEndClientId { get; set; }
        public string Instance { get; set; }
        public string InviteRedirect { get; set; }
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string SignUpSignInPolicyId { get; set; }
        public string SignedOutCallbackPath { get; set; }
        public string ResetPasswordPolicyId { get; set; }
        public string ADApplicationB2CId { get; set; }
    }
}
