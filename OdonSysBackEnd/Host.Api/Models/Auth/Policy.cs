namespace Host.Api.Models.Auth
{
    sealed class Policy
    {
        public const string CanAccessClient = "can_access_client";
        public const string CanManageClient = "can_manage_client";
        public const string CanDeleteClient = "can_delete_client";
        public const string CanAssignClient = "can_assign_client";
        public const string CanAccessMyClients = "can_access_my_clients";

        public const string CanAccessRole = "can_access_role";
        public const string CanManageRole = "can_manage_role";

        public const string CanAccessProcedure = "can_access_procedure";
        public const string CanManageProcedure = "can_manage_procedure";

        public const string CanAccessDoctor = "can_access_doctor";
        public const string CanUpdateDoctor = "can_update_doctor";
        public const string CanDeleteDoctor = "can_delete_doctor";

        public const string CanApproveDoctor = "can_approve_doctor";

    }
}
