namespace Host.SignUpSignIn.Models
{
    internal class AzureActiveB2CApiRequest
    {
        public string Step { get; set; }
        public string Client_Id { get; set; }
        public string Ui_locales { get; set; }
        public string Email { get; set; }
        public string ObjectId { get; set; }
        public string Surname { get; set; }
        public string DisplayName { get; set; }
        public string GivenName { get; set; }
        public string Roles { get; set; }
    }
}
