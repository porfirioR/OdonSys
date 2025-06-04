import { Routes } from '@angular/router';
import { ClientsComponent } from '../core/components/my-clients/my-clients.component';
import { UpsertClientComponent } from '../core/components/upsert-client/upsert-client.component';
import { MyConfigurationComponent } from './components/my-configuration/my-configuration.component';
import { InvoicesComponent } from './components/invoices/invoices.component';
import { RegisterInvoiceComponent } from './components/register-invoice/register-invoice.component';
import { ShowInvoiceComponent } from './components/show-invoice/show-invoice.component';
import { PermissionGuard } from '../core/guards/permission.guard';
import { PreventUnsavedChangesWorkspace } from './guards/prevent-unsaved-changes-workspace.guard';
import { Permission } from '../core/enums/permission.enum';
import { ClientDetailComponent } from '../core/components/client-detail/client-detail.component';
import { UpdateInvoiceComponent } from './components/update-invoice/update-invoice.component';
import { ClientReportComponent } from '../core/components/client-report/client-report.component';
import { OrthodonticsComponent } from './components/orthodontics/orthodontics.component';
import { UpsertOrthodonticComponent } from '../core/components/upsert-orthodontic/upsert-orthodontic.component';

export const WorkspaceRoutes: Routes = [
  { 
    path: '',
    children: [
      { path: 'configuración/datos',
        component: MyConfigurationComponent,
        title: 'Mis datos',
        canActivate: [PermissionGuard],
        canDeactivate: [PreventUnsavedChangesWorkspace],
        data: { permissions: [ Permission.AccessMyData ] }
      },
      {
        path: 'mis-pacientes',
        component: ClientsComponent,
        title: 'Mis pacientes',
        canActivate: [PermissionGuard],
        data: { permissions: [ Permission.AccessMyClients ] }
      },
      {
        path: 'mis-pacientes/registrar',
        component: UpsertClientComponent,
        title: 'Registrar paciente',
        canDeactivate: [PreventUnsavedChangesWorkspace],
        canActivate: [PermissionGuard],
        data: { permissions: [ Permission.AccessMyClients, Permission.CreateClients ] }
      },
      {
        path: 'mis-pacientes/actualizar/:id',
        component: UpsertClientComponent,
        title: 'Actualizar mi paciente',
        canDeactivate: [PreventUnsavedChangesWorkspace],
        canActivate: [PermissionGuard],
        data: { permissions: [ Permission.AccessMyClients, Permission.UpdateClients ] }
      },
      {
        path: 'mis-pacientes/ver/:id',
        component: ClientDetailComponent,
        title: 'Datos del paciente',
        data: { permissions: [ Permission.AccessMyClients ] }
      },
      {
        path: 'mis-pacientes/reporte/:id',
        component: ClientReportComponent,
        title: 'Reporte del paciente',
        data: { permissions: [ Permission.AccessMyClients ] }
      },
      {
        path: 'facturas',
        component: InvoicesComponent,
        canActivate: [PermissionGuard],
        title: 'Facturas',
        data: { permissions: [ Permission.AccessInvoices ] }
      },
      {
        path: 'mis-facturas',
        component: InvoicesComponent,
        canActivate: [PermissionGuard],
        title: 'Mis Facturas',
        data: { permissions: [ Permission.AccessMyInvoices ] }
      },
      {
        path: 'mis-facturas/registrar',
        component: RegisterInvoiceComponent,
        canActivate: [PermissionGuard],
        canDeactivate: [PreventUnsavedChangesWorkspace],
        title: 'Registrar Factura',
        data: { permissions: [ Permission.CreateInvoices ] }
      },
      {
        path: 'mis-facturas/ver/:id',
        component: ShowInvoiceComponent,
        canActivate: [PermissionGuard],
        title: 'Ver factura',
        data: { permissions: [ Permission.AccessMyInvoices ] },
      },
      {
        path: 'mis-facturas/actualizar/:id',
        canActivate: [PermissionGuard],
        component: UpdateInvoiceComponent,
        title: 'Actualizar factura',
        data: { permissions: [ Permission.UpdateInvoices ] }
      },
      {
        path: 'facturas/registrar',
        component: RegisterInvoiceComponent,
        canActivate: [PermissionGuard],
        canDeactivate: [PreventUnsavedChangesWorkspace],
        title: 'Registrar Factura',
        data: { permissions: [ Permission.CreateInvoices ] }
      },
      {
        path: 'facturas/ver/:id',
        canActivate: [PermissionGuard],
        component: ShowInvoiceComponent,
        title: 'Ver factura',
        data: { permissions: [ Permission.AccessInvoices ] }
      },
      {
        path: 'facturas/actualizar/:id',
        canActivate: [PermissionGuard],
        component: UpdateInvoiceComponent,
        title: 'Actualizar factura',
        data: { permissions: [ Permission.UpdateInvoices ] }
      },
      {
        path: 'ortodoncias',
        canActivate: [PermissionGuard],
        component: OrthodonticsComponent,
        title: 'Ortodoncias',
        data: { permissions: [ Permission.AccessAllOrthodontics ] }
      },
      {
        path: 'ortodoncias/registrar',
        canActivate: [PermissionGuard],
        component: UpsertOrthodonticComponent,
        title: 'Registrar Ortodoncia',
        data: { permissions: [ Permission.CreateOrthodontics ] }
      },
      {
        path: 'ortodoncias/actualizar/:id',
        canActivate: [PermissionGuard],
        component: UpsertOrthodonticComponent,
        title: 'Registrar Ortodoncia',
        data: { permissions: [ Permission.UpdateOrthodontics ] }
      },
      {
        path: 'mis-pacientes/mis-ortodoncias/:clientId',
        canActivate: [PermissionGuard],
        component: OrthodonticsComponent,
        title: 'Mis Ortodoncias',
        data: { permissions: [ Permission.AccessOrthodontics, Permission.AccessMyClients ] }
      },
      {
        path: 'mis-pacientes/mis-ortodoncias/:clientId/registrar',
        canActivate: [PermissionGuard],
        component: UpsertOrthodonticComponent,
        title: 'Registrar Ortodoncia',
        data: { permissions: [ Permission.CreateOrthodontics, Permission.AccessMyClients, Permission.AccessOrthodontics ] }
      },
      {
        path: 'mis-pacientes/mis-ortodoncias/:clientId/actualizar/:id',
        canActivate: [PermissionGuard],
        component: UpsertOrthodonticComponent,
        title: 'Actualizar Ortodoncia',
        data: { permissions: [ Permission.UpdateOrthodontics, Permission.AccessMyClients, Permission.AccessOrthodontics ] }
      },
    ]
  }
]
