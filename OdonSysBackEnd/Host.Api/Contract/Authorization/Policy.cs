﻿namespace Host.Api.Contract.Authorization;

sealed class Policy
{
    public const string CanAccessClient = "can_access_client";
    public const string CanManageClient = "can_manage_client";
    public const string CanDeleteClient = "can_delete_client";
    public const string CanAssignClient = "can_assign_client";
    public const string CanAccessMyClients = "can_access_my_clients";
    public const string CanModifyVisibilityClient = "can_modify_visibility_client";

    public const string CanAccessInvoice = "can_access_invoice";
    public const string CanAccessTeeth = "can_access_teeth";
    public const string CanAccessMyInvoice = "can_access_my_invoice";
    public const string CanAccessInvoiceFiles = "can_access_invoice_files";
    public const string CanCreateInvoice = "can_create_invoice";
    public const string CanUpdateInvoice = "can_update_invoice";
    public const string CanChangeInvoiceStatus = "can_change_invoice_status";

    public const string CanAccessRole = "can_access_role";
    public const string CanManageRole = "can_manage_role";
    public const string CanAssignRoleDoctors = "can_assign_role_doctors";

    public const string CanAccessProcedure = "can_access_procedure";
    public const string CanManageProcedure = "can_manage_procedure";
    public const string CanModifyVisibilityProcedure = "can_modify_visibility_procedure";

    public const string CanAccessDoctor = "can_access_doctor";
    public const string CanUpdateDoctor = "can_update_doctor";
    public const string CanDeleteDoctor = "can_delete_doctor";
    public const string CanAssignDoctorRoles = "can_assign_doctor_roles";
    public const string CanModifyVisibilityDoctor = "can_modify_visibility_doctor";

    public const string CanAccessOrthodontic = "can_access_orthodontic";
    public const string CanCreateOrthodontic = "can_create_orthodontic";
    public const string CanUpdateOrthodontic = "can_update_orthodontic";
    public const string CanDeleteOrthodontic = "can_delete_orthodontic";
    public const string CanAccessAllOrthodontics = "can_access_all_orthodontics";

    public const string CanCreateClientProcedure = "can_create_client_procedure";
    public const string CanUpdateClientProcedure = "can_update_client_procedure";

    public const string CanApproveDoctor = "can_approve_doctor";

    public const string CanAccessPayment = "can_access_payment";
    public const string CanRegisterPayment = "can_register_payment";
}
