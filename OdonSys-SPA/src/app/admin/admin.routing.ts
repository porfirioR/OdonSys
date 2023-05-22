import { Routes } from '@angular/router';
import { UpsertClientComponent } from '../core/components/clients/upsert-client/upsert-client.component';
import { AdminClientsComponent } from './components/admin-clients/admin-clients.component';
import { AdminProcedureComponent } from './components/admin-procedure/admin-procedure.component';
import { UpsertProcedureComponent } from './components/admin-procedure/upsert-procedure/upsert-procedure.component';
import { DoctorsComponent } from './components/doctors/doctors.component';
import { RolesComponent } from './components/roles/roles.component';
import { UpsertRoleComponent } from './components/roles/upsert-role/upsert-role.component';
import { PermissionGuard } from '../core/guards/permission.guard';
import { PreventUnsavedChanges } from './guards/prevent-unsaved-changes.guard';
import { Permission } from '../core/enums/permission.enum';

export const AdminRoutes: Routes = [
  { 
    path: '',
    children: [
      {
        path: 'procedimientos',
        component: AdminProcedureComponent,
        canActivate: [PermissionGuard],
        title: 'Procedimientos',
        data: { permissions: [ Permission.AccessProcedures ] }
      },
      {
        path: 'procedimientos/crear',
        component: UpsertProcedureComponent,
        canActivate: [PermissionGuard],
        canDeactivate: [PreventUnsavedChanges],
        title: 'Crear Procedimientos',
        data: { permissions: [ Permission.CreateProcedures ] }
      },
      {
        path: 'procedimientos/actualizar/:id',
        component: UpsertProcedureComponent,
        canActivate: [PermissionGuard],
        canDeactivate: [PreventUnsavedChanges],
        title: 'Actualizar Procedimientos',
        data: { permissions: [ Permission.UpdateProcedures ] } },
      {
        path: 'doctores',
        component: DoctorsComponent,
        canActivate: [PermissionGuard],
        title: 'Doctores',
        data: { permissions: [ Permission.AccessDoctors ] }
      },
      {
        path: 'pacientes',
        component: AdminClientsComponent,
        title: 'Pacientes',
        data: { permissions: [ Permission.AccessClients ] }
      },
      {
        path: 'pacientes/ver/:id',
        component: AdminClientsComponent,
        title: 'Ver pacientes',
        data: { permissions: [ Permission.CreateProcedures ] }
      },
      {
        path: 'pacientes/registrar',
        component: UpsertClientComponent,
        canActivate: [PermissionGuard],
        canDeactivate: [PreventUnsavedChanges],
        title: 'Actualizar pacientes',
        data: { permissions: [ Permission.CreateClients ] }
      },
      {
        path: 'pacientes/actualizar/:id',
        component: UpsertClientComponent,
        canActivate: [PermissionGuard],
        title: 'Actualizar pacientes',
        data: { permissions: [ Permission.UpdateClients ] }
      },
      {
        path: 'roles',
        component: RolesComponent,
        canActivate: [PermissionGuard],
        title: 'Roles',
        data: { permissions: [ Permission.AccessRoles ] }
      },
      {
        path: 'roles/crear',
        component: UpsertRoleComponent,
        canActivate: [PermissionGuard],
        canDeactivate: [PreventUnsavedChanges],
        title: 'Crear Roles',
        data: { permissions: [ Permission.ManageRoles ] }
      },
      {
        path: 'roles/actualizar/:code',
        component: UpsertRoleComponent,
        canActivate: [PermissionGuard],
        canDeactivate: [PreventUnsavedChanges],
        title: 'Actualizar Roles',
        data: { permissions: [ Permission.ManageRoles ] }
      }
    ]
  }
]
