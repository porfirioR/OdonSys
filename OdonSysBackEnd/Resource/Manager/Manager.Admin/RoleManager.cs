using Access.Contract.Roles;
using AutoMapper;
using Contract.Admin.Roles;
using Utilities.Enums;

namespace Manager.Admin
{
    internal class RoleManager : IRoleManager
    {
        private readonly IMapper _mapper;
        private readonly IRoleAccess _roleAccess;

        public RoleManager(IMapper mapper, IRoleAccess roleAccess)
        {
            _mapper = mapper;
            _roleAccess = roleAccess;
        }

        public async Task<RoleModel> CreateAsync(CreateRoleRequest request)
        {
            var accessRequest = _mapper.Map<CreateRoleAccessRequest>(request);
            var result = await _roleAccess.CreateAccessAsync(accessRequest);
            return _mapper.Map<RoleModel>(result);
        }

        public async Task<IEnumerable<RoleModel>> GetAllAsync()
        {
            var result = await _roleAccess.GetAllAccessAsync();
            return _mapper.Map<IEnumerable<RoleModel>>(result);
        }

        public IEnumerable<PermissionModel> GetAllPermissions()
        {
            return new List<PermissionModel>()
            {
                new PermissionModel("Ingresar", PermissionName.AccessInvoices, PermissionGroup.Admin, PermissionSubGroup.Invoice),
                new PermissionModel("Mis Facturas", PermissionName.AccessMyInvoices, PermissionGroup.Work, PermissionSubGroup.Invoice),
                new PermissionModel("Crear Factura", PermissionName.CreateInvoices, PermissionGroup.Work, PermissionSubGroup.Invoice),
                new PermissionModel("Cambiar Estado", PermissionName.ChangeInvoiceStatus, PermissionGroup.Work, PermissionSubGroup.Invoice),

                new PermissionModel("Ingresar", PermissionName.AccessRoles, PermissionGroup.Admin, PermissionSubGroup.Role),
                new PermissionModel("Administrar", PermissionName.ManageRoles, PermissionGroup.Admin, PermissionSubGroup.Role),
                new PermissionModel("Asignar Usuarios", PermissionName.AssignRoleDoctors, PermissionGroup.Admin, PermissionSubGroup.Role),

                new PermissionModel("Ingresar", PermissionName.AccessClients, PermissionGroup.Work, PermissionSubGroup.Client),
                new PermissionModel("Crear", PermissionName.CreateClients, PermissionGroup.Admin, PermissionSubGroup.Client),
                new PermissionModel("Actualizar", PermissionName.UpdateClients, PermissionGroup.Admin, PermissionSubGroup.Client),
                new PermissionModel("Asignar", PermissionName.AssignClients, PermissionGroup.Admin, PermissionSubGroup.Client),
                new PermissionModel("Borrar", PermissionName.DeleteClients, PermissionGroup.Admin, PermissionSubGroup.Client),
                new PermissionModel("Mis clientes", PermissionName.AccessMyClients, PermissionGroup.Work, PermissionSubGroup.Client),
                new PermissionModel("Editar Todos los campos", PermissionName.FullFieldUpdateClients, PermissionGroup.Work, PermissionSubGroup.Client),
                new PermissionModel("Restaurar", PermissionName.RestoreClients, PermissionGroup.Admin, PermissionSubGroup.Client),
                new PermissionModel("Inhabilitar", PermissionName.DeactivateClients, PermissionGroup.Admin, PermissionSubGroup.Client),

                new PermissionModel("Ingresar", PermissionName.AccessProcedures, PermissionGroup.Work, PermissionSubGroup.Procedure),
                new PermissionModel("Crear", PermissionName.CreateProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),
                new PermissionModel("Actualizar", PermissionName.UpdateProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),
                new PermissionModel("Borrar", PermissionName.DeleteProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),
                new PermissionModel("Restaurar", PermissionName.RestoreProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),
                new PermissionModel("Inhabilitar", PermissionName.DeactivateProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),

                new PermissionModel("Ingresar", PermissionName.AccessDoctors, PermissionGroup.Work, PermissionSubGroup.Doctor),
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

        public async Task<IEnumerable<string>> GetPermissonsByRolesAsync(IEnumerable<string> roles)
        {
            var allPermissions = (await GetAllAsync()).Where(x => roles.Contains(x.Code)).SelectMany(x => x.RolePermissions);
            return allPermissions.Distinct();
        }

        public async Task<RoleModel> GetRoleByCodeAsync(string code)
        {
            var result = await _roleAccess.GetRoleByCodeAccessAsync(code);
            return _mapper.Map<RoleModel>(result);
        }

        public async Task<RoleModel> UpdateAsync(UpdateRoleRequest request)
        {
            var accessRequest = _mapper.Map<UpdateRoleAccessRequest>(request);
            var result = await _roleAccess.UpdateAccessAsync(accessRequest);
            return _mapper.Map<RoleModel>(result);
        }
    }
}
