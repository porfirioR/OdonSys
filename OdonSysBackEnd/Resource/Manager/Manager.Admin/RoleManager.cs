using Access.Contract.Roles;
using AutoMapper;
using Contract.Admin.Roles;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                new PermissionModel("Acceso", PermissionName.AccessRoles, PermissionGroup.Admin, PermissionSubGroup.Role),
                new PermissionModel("Administrar", PermissionName.ManageRoles, PermissionGroup.Admin, PermissionSubGroup.Role),

                new PermissionModel("Acceso", PermissionName.AccessClients, PermissionGroup.Work, PermissionSubGroup.Client),
                new PermissionModel("Crear", PermissionName.CreateClients, PermissionGroup.Admin, PermissionSubGroup.Client),
                new PermissionModel("Actualizar", PermissionName.UpdateClients, PermissionGroup.Admin, PermissionSubGroup.Client),
                new PermissionModel("Asignar", PermissionName.AssignClients, PermissionGroup.Admin, PermissionSubGroup.Client),
                new PermissionModel("Borrar", PermissionName.DeleteClients, PermissionGroup.Admin, PermissionSubGroup.Client),
                new PermissionModel("Mis clientes", PermissionName.AssignClients, PermissionGroup.Work, PermissionSubGroup.Client),

                new PermissionModel("Acceso", PermissionName.AccessProcedures, PermissionGroup.Work, PermissionSubGroup.Procedure),
                new PermissionModel("Crear", PermissionName.CreateProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),
                new PermissionModel("Actualizar", PermissionName.UpdateProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),
                new PermissionModel("Borrar", PermissionName.DeleteProcedures, PermissionGroup.Admin, PermissionSubGroup.Procedure),

                new PermissionModel("Acceso", PermissionName.AccessDoctors, PermissionGroup.Work, PermissionSubGroup.Doctor),
                new PermissionModel("Crear", PermissionName.CreateDoctors, PermissionGroup.Admin, PermissionSubGroup.Doctor),
                new PermissionModel("Actualizar", PermissionName.UpdateDoctors, PermissionGroup.Admin, PermissionSubGroup.Doctor),
                new PermissionModel("Borrar", PermissionName.DeleteDoctors, PermissionGroup.Admin, PermissionSubGroup.Doctor),
            };
        }

        public Task<IEnumerable<string>> GetPermissonsByRolesAsync(IEnumerable<string> roles)
        {
            throw new NotImplementedException();
        }

        public Task<RoleModel> GetRoleByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task<RoleModel> UpdateAsync(UpdateRoleRequest request)
        {
            var accessRequest = _mapper.Map<UpdateRoleAccessRequest>(request);
            var result = await _roleAccess.UpdateAccessAsync(accessRequest);
            return _mapper.Map<RoleModel>(result);
        }
    }
}
