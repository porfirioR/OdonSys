import { Routes } from '@angular/router';
import { UpsertClientComponent } from '../core/components/upsert-client/upsert-client.component';
import { AdminClientsComponent } from './components/admin-clients/admin-clients.component';
import { AdminProcedureComponent } from './components/admin-procedure/admin-procedure.component';
import { UpsertProcedureComponent } from './components/upsert-procedure/upsert-procedure.component';
import { DoctorsComponent } from './components/doctors/doctors.component';
import { RolesComponent } from './components/roles/roles.component';
import { UpsertRoleComponent } from './components/upsert-role/upsert-role.component';
import { PermissionGuard } from '../core/guards/permission.guard';
import { PreventUnsavedChangesAdmin } from './guards/prevent-unsaved-changes-admin.guard';
import { Permission } from '../core/enums/permission.enum';
import { ClientDetailComponent } from '../core/components/client-detail/client-detail.component';
import { ClientReportComponent } from '../core/components/client-report/client-report.component';
import { OrthodonticsComponent } from '../workspace/components/orthodontics/orthodontics.component';
import { UpsertOrthodonticComponent } from '../core/components/upsert-orthodontic/upsert-orthodontic.component';

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
        canDeactivate: [PreventUnsavedChangesAdmin],
        title: 'Crear Procedimientos',
        data: { permissions: [ Permission.CreateProcedures ] }
      },
      {
        path: 'procedimientos/actualizar/:id',
        component: UpsertProcedureComponent,
        canActivate: [PermissionGuard],
        canDeactivate: [PreventUnsavedChangesAdmin],
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
        component: ClientDetailComponent,
        title: 'Datos del paciente',
        data: { permissions: [ Permission.AccessClients ] }
      },
      {
        path: 'pacientes/reporte/:id',
        component: ClientReportComponent,
        title: 'Reporte del paciente',
        data: { permissions: [ Permission.AccessClients ] }
      },
      {
        path: 'pacientes/registrar',
        component: UpsertClientComponent,
        canActivate: [PermissionGuard],
        canDeactivate: [PreventUnsavedChangesAdmin],
        title: 'Registrar paciente',
        data: { permissions: [ Permission.CreateClients ] }
      },
      {
        path: 'pacientes/actualizar/:id',
        component: UpsertClientComponent,
        canActivate: [PermissionGuard],
        canDeactivate: [PreventUnsavedChangesAdmin],
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
        canDeactivate: [PreventUnsavedChangesAdmin],
        title: 'Crear Roles',
        data: { permissions: [ Permission.ManageRoles ] }
      },
      {
        path: 'roles/actualizar/:code',
        component: UpsertRoleComponent,
        canActivate: [PermissionGuard],
        canDeactivate: [PreventUnsavedChangesAdmin],
        title: 'Actualizar Roles',
        data: { permissions: [ Permission.ManageRoles ] }
      },
      {
        path: 'pacientes/ortodoncias/:clientId',
        canActivate: [PermissionGuard],
        component: OrthodonticsComponent,
        title: 'Mis Ortodoncias',
        data: { permissions: [ Permission.AccessOrthodontics, Permission.AccessClients ] }
      },
      {
        path: 'pacientes/ortodoncias/:clientId/registrar',
        canActivate: [PermissionGuard],
        component: UpsertOrthodonticComponent,
        title: 'Registrar Ortodoncia',
        data: { permissions: [ Permission.CreateOrthodontics, Permission.AccessClients ] }
      },
      {
        path: 'pacientes/ortodoncias/:clientId/actualizar/:id',
        canActivate: [PermissionGuard],
        component: UpsertOrthodonticComponent,
        title: 'Actualizar Ortodoncia',
        data: { permissions: [ Permission.UpdateOrthodontics, Permission.AccessClients ] }
      },
    ]
  }
]
