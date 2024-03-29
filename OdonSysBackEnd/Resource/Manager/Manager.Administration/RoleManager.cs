﻿using Access.Contract.Roles;
using Access.Contract.Users;
using Contract.Administration.Roles;
using Utilities.Enums;

namespace Manager.Administration
{
    internal sealed class RoleManager : IRoleManager
    {
        private readonly IRoleAccess _roleAccess;
        private readonly IRoleManagerBuilder _roleManagerBuilder;
        private readonly IUserDataAccess _userDataAccess;

        public RoleManager(
            IRoleAccess roleAccess,
            IRoleManagerBuilder roleManagerBuilder,
            IUserDataAccess userDataAccess
        )
        {
            _roleAccess = roleAccess;
            _roleManagerBuilder = roleManagerBuilder;
            _userDataAccess = userDataAccess;
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
            return new List<PermissionModel>()
            {
                new PermissionModel("Ingresar", PermissionName.AccessInvoices, PermissionGroup.Admin, PermissionSubGroup.Invoice),
                new PermissionModel("Mis Facturas", PermissionName.AccessMyInvoices, PermissionGroup.Work, PermissionSubGroup.Invoice),
                new PermissionModel("Crear Facturas", PermissionName.CreateInvoices, PermissionGroup.Work, PermissionSubGroup.Invoice),
                new PermissionModel("Actualizar Facturas", PermissionName.UpdateInvoices, PermissionGroup.Work, PermissionSubGroup.Invoice),
                new PermissionModel("Cambiar Estado", PermissionName.ChangeInvoiceStatus, PermissionGroup.Work, PermissionSubGroup.Invoice),

                new PermissionModel("Ingresar", PermissionName.AccessRoles, PermissionGroup.Admin, PermissionSubGroup.Role),
                new PermissionModel("Administrar", PermissionName.ManageRoles, PermissionGroup.Admin, PermissionSubGroup.Role),
                new PermissionModel("Asignar Usuarios", PermissionName.AssignRoleDoctors, PermissionGroup.Admin, PermissionSubGroup.Role),

                new PermissionModel("Ingresar", PermissionName.AccessClients, PermissionGroup.Work, PermissionSubGroup.Client),
                new PermissionModel("Crear", PermissionName.CreateClients, PermissionGroup.Admin, PermissionSubGroup.Client),
                new PermissionModel("Actualizar", PermissionName.UpdateClients, PermissionGroup.Admin, PermissionSubGroup.Client),
                new PermissionModel("Asignar", PermissionName.AssignClients, PermissionGroup.Admin, PermissionSubGroup.Client),
                new PermissionModel("Borrar", PermissionName.DeleteClients, PermissionGroup.Admin, PermissionSubGroup.Client),
                new PermissionModel("Mis Pacientes", PermissionName.AccessMyClients, PermissionGroup.Work, PermissionSubGroup.Client),
                new PermissionModel("Editar todos los campos", PermissionName.FullFieldUpdateClients, PermissionGroup.Work, PermissionSubGroup.Client),
                new PermissionModel("Restaurar", PermissionName.RestoreClients, PermissionGroup.Admin, PermissionSubGroup.Client),
                new PermissionModel("Inhabilitar", PermissionName.DeactivateClients, PermissionGroup.Admin, PermissionSubGroup.Client),

                new PermissionModel("Ingresar", PermissionName.AccessProcedures, PermissionGroup.Work, PermissionSubGroup.Procedure),
                new PermissionModel("Crear", PermissionName.CreateProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),
                new PermissionModel("Actualizar", PermissionName.UpdateProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),
                new PermissionModel("Borrar", PermissionName.DeleteProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),
                new PermissionModel("Restaurar", PermissionName.RestoreProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),
                new PermissionModel("Inhabilitar", PermissionName.DeactivateProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),

                new PermissionModel("Ingresar", PermissionName.AccessDoctors, PermissionGroup.Work, PermissionSubGroup.Doctor),
                new PermissionModel("Ver mis datos", PermissionName.AccessMyData, PermissionGroup.Work, PermissionSubGroup.Doctor),
                new PermissionModel("Aprobar ingresar", PermissionName.ApproveDoctors, PermissionGroup.Admin, PermissionSubGroup.Doctor),
                new PermissionModel("Actualizar", PermissionName.UpdateDoctors, PermissionGroup.Admin, PermissionSubGroup.Doctor),
                new PermissionModel("Borrar", PermissionName.DeleteDoctors, PermissionGroup.Admin, PermissionSubGroup.Doctor),
                new PermissionModel("Asignar Roles", PermissionName.AssignDoctorRoles, PermissionGroup.Admin, PermissionSubGroup.Doctor),
                new PermissionModel("Inhabilitar", PermissionName.DeactivateDoctors, PermissionGroup.Admin, PermissionSubGroup.Doctor),
                new PermissionModel("Restaurar", PermissionName.RestoreDoctors, PermissionGroup.Admin, PermissionSubGroup.Doctor),

                new PermissionModel("Ingresar", PermissionName.AccessPayments, PermissionGroup.Work, PermissionSubGroup.Payment),
                new PermissionModel("Registrar Pago", PermissionName.RegisterPayments, PermissionGroup.Work, PermissionSubGroup.Payment),
            };
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
}
