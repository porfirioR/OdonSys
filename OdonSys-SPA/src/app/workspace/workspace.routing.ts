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
        title: 'Actualizar paciente',
        canDeactivate: [PreventUnsavedChangesWorkspace],
        canActivate: [PermissionGuard],
        data: { permissions: [ Permission.AccessMyClients, Permission.UpdateClients ] }
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
        title: 'Actualizar paciente'
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
        component: ShowInvoiceComponent,
        title: 'Actualizar paciente'
      }
    ]
  }
]
