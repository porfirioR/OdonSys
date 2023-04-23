namespace Utilities.Enums
{
    public enum PermissionName
    {
        #region Roles
        AccessRoles,
        ManageRoles,
        #endregion

        #region Clients
        AccessClients,
        CreateClients,
        UpdateClients,
        DeleteClients,
        AssignClients,
        AccessMyClients,
        #endregion

        #region Procedures
        AccessProcedures,
        CreateProcedures,
        UpdateProcedures,
        DeleteProcedures,
        RestoreProcedures,
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
        ApproveDoctors
        #endregion
    }
}
