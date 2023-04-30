namespace Utilities.Enums
{
    public enum PermissionName
    {
        #region Roles
        AccessRoles,
        ManageRoles,
        AssignRoleDoctors,
        #endregion

        #region Clients
        AccessClients,
        CreateClients,
        UpdateClients,
        DeactivateClients,
        RestoreClients,
        DeleteClients,
        AssignClients,
        AccessMyClients,
        FullFieldUpdateClients,
        #endregion

        #region Procedures
        AccessProcedures,
        CreateProcedures,
        UpdateProcedures,
        DeleteProcedures,
        RestoreProcedures,
        DeactivateProcedures,
        #endregion

        #region Client Procedures
        AccessClientProcedures,
        CreateClientProcedures,
        UpdateClientProcedures,
        #endregion

        #region Doctors
        AccessDoctors,
        UpdateDoctors,
        DeleteDoctors,
        RestoreDoctors,
        DeactivateDoctors,
        ApproveDoctors,
        AssignDoctorRoles
        #endregion
    }
}
