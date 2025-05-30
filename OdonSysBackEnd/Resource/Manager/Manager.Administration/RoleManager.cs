using Access.Contract.Roles;
using Contract.Administration.Roles;
using Utilities.Enums;

namespace Manager.Administration;

internal sealed class RoleManager : IRoleManager
{
    private readonly IRoleAccess _roleAccess;
    private readonly IRoleManagerBuilder _roleManagerBuilder;

    public RoleManager(
        IRoleAccess roleAccess,
        IRoleManagerBuilder roleManagerBuilder
    )
    {
        _roleAccess = roleAccess;
        _roleManagerBuilder = roleManagerBuilder;
    }

    public async Task<RoleModel> CreateAsync(CreateRoleRequest request)
    {
        var accessRequest = _roleManagerBuilder.MapCreateRoleRequestToCreateRoleAccessRequest(request);
        var result = await _roleAccess.CreateAccessAsync(accessRequest);
        return _roleManagerBuilder.MapRoleAccessModelToRoleModel(result);
    }

    public async Task<IEnumerable<RoleModel>> GetAllAsync()
    {
        var accessModelList = await _roleAccess.GetAllAccessAsync();
        return accessModelList.Select(_roleManagerBuilder.MapRoleAccessModelToRoleModel);
    }

    public IEnumerable<PermissionModel> GetAllPermissions()
    {
        return
        [
            new("Ingresar", PermissionName.AccessInvoices, PermissionGroup.Admin, PermissionSubGroup.Invoice),
            new("Mis Facturas", PermissionName.AccessMyInvoices, PermissionGroup.Work, PermissionSubGroup.Invoice),
            new("Crear Facturas", PermissionName.CreateInvoices, PermissionGroup.Work, PermissionSubGroup.Invoice),
            new("Actualizar Facturas", PermissionName.UpdateInvoices, PermissionGroup.Work, PermissionSubGroup.Invoice),
            new("Cambiar Estado", PermissionName.ChangeInvoiceStatus, PermissionGroup.Work, PermissionSubGroup.Invoice),

            new("Ingresar", PermissionName.AccessRoles, PermissionGroup.Admin, PermissionSubGroup.Role),
            new("Administrar", PermissionName.ManageRoles, PermissionGroup.Admin, PermissionSubGroup.Role),
            new("Asignar Usuarios", PermissionName.AssignRoleDoctors, PermissionGroup.Admin, PermissionSubGroup.Role),

            new("Ingresar", PermissionName.AccessClients, PermissionGroup.Work, PermissionSubGroup.Client),
            new("Crear", PermissionName.CreateClients, PermissionGroup.Admin, PermissionSubGroup.Client),
            new("Actualizar", PermissionName.UpdateClients, PermissionGroup.Admin, PermissionSubGroup.Client),
            new("Asignar", PermissionName.AssignClients, PermissionGroup.Admin, PermissionSubGroup.Client),
            new("Borrar", PermissionName.DeleteClients, PermissionGroup.Admin, PermissionSubGroup.Client),
            new("Mis Pacientes", PermissionName.AccessMyClients, PermissionGroup.Work, PermissionSubGroup.Client),
            new("Editar todos los campos", PermissionName.FullFieldUpdateClients, PermissionGroup.Work, PermissionSubGroup.Client),
            new("Restaurar", PermissionName.RestoreClients, PermissionGroup.Admin, PermissionSubGroup.Client),
            new("Inhabilitar", PermissionName.DeactivateClients, PermissionGroup.Admin, PermissionSubGroup.Client),

            new("Ingresar", PermissionName.AccessProcedures, PermissionGroup.Work, PermissionSubGroup.Procedure),
            new("Crear", PermissionName.CreateProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),
            new("Actualizar", PermissionName.UpdateProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),
            new("Borrar", PermissionName.DeleteProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),
            new("Restaurar", PermissionName.RestoreProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),
            new("Inhabilitar", PermissionName.DeactivateProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),

            new("Ingresar", PermissionName.AccessDoctors, PermissionGroup.Work, PermissionSubGroup.Doctor),
            new("Ver mis datos", PermissionName.AccessMyData, PermissionGroup.Work, PermissionSubGroup.Doctor),
            new("Aprobar ingresar", PermissionName.ApproveDoctors, PermissionGroup.Admin, PermissionSubGroup.Doctor),
            new("Actualizar", PermissionName.UpdateDoctors, PermissionGroup.Admin, PermissionSubGroup.Doctor),
            new("Borrar", PermissionName.DeleteDoctors, PermissionGroup.Admin, PermissionSubGroup.Doctor),
            new("Asignar Roles", PermissionName.AssignDoctorRoles, PermissionGroup.Admin, PermissionSubGroup.Doctor),
            new("Inhabilitar", PermissionName.DeactivateDoctors, PermissionGroup.Admin, PermissionSubGroup.Doctor),
            new("Restaurar", PermissionName.RestoreDoctors, PermissionGroup.Admin, PermissionSubGroup.Doctor),

            new("Ingresar", PermissionName.AccessPayments, PermissionGroup.Work, PermissionSubGroup.Payment),
            new("Registrar Pago", PermissionName.RegisterPayments, PermissionGroup.Work, PermissionSubGroup.Payment),

            new("Ingresar al del Paciente", PermissionName.AccessOrthodontics, PermissionGroup.Work, PermissionSubGroup.Orthodontic),
            new("Ingresar a todos", PermissionName.AccessAllOrthodontics, PermissionGroup.Work, PermissionSubGroup.Orthodontic),
            new("Crear", PermissionName.CreateOrthodontics, PermissionGroup.Work, PermissionSubGroup.Orthodontic),
            new("Borrar", PermissionName.DeleteOrthodontics, PermissionGroup.Work, PermissionSubGroup.Orthodontic),
            new("Actualizar", PermissionName.UpdateOrthodontics, PermissionGroup.Work, PermissionSubGroup.Orthodontic),
        ];
    }

    public async Task<IEnumerable<string>> GetPermissionsByUserIdAsync(string userId)
    {
        var roles = (await _roleAccess.GetRolesByUserIdAsync(userId)).Select(x => x.Code);
        var allPermissions = (await GetAllAsync()).Where(x => roles.Contains(x.Code)).SelectMany(x => x.RolePermissions);
        return allPermissions.Distinct();
    }

    public async Task<RoleModel> GetRoleByCodeAsync(string code)
    {
        var accessModelList = await _roleAccess.GetRoleByCodeAccessAsync(code);
        return _roleManagerBuilder.MapRoleAccessModelToRoleModel(accessModelList);
    }

    public async Task<RoleModel> UpdateAsync(UpdateRoleRequest request)
    {
        var accessRequest = _roleManagerBuilder.MapUpdateRoleRequestToUpdateRoleAccessRequest(request);
        var accessModelList = await _roleAccess.UpdateAccessAsync(accessRequest);
        return _roleManagerBuilder.MapRoleAccessModelToRoleModel(accessModelList);
    }
}
