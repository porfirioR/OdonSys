namespace Host.Api.Models.Auth
{
    sealed class Policy
    {
        public const string CanAccessClients = "can_access_clients";
        public const string CanManageClients = "can_manage_clients";
        public const string CanDeleteClients = "can_delete_clients";
        public const string CanAccessRole = "can_access_role";
        public const string CanManageRole = "can_manage_role";
    }
}
