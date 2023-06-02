import { Routes } from '@angular/router';
import { ClientsComponent } from '../core/components/clients/clients.component';
import { UpsertClientComponent } from '../core/components/upsert-client/upsert-client.component';
import { MyConfigurationComponent } from './components/my-configuration/my-configuration.component';
import { InvoicesComponent } from './components/invoices/invoices.component';
import { RegisterInvoiceComponent } from './components/register-invoice/register-invoice.component';
import { PermissionGuard } from '../core/guards/permission.guard';
import { PreventUnsavedChangesWorkspace } from './guards/prevent-unsaved-changes-workspace.guard';
import { PreventUnsavedChangesAdmin } from '../admin/guards/prevent-unsaved-changes-admin.guard';
import { Permission } from '../core/enums/permission.enum';
import { ShowInvoiceComponent } from './components/show-invoice/show-invoice.component';

export const WorkspaceRoutes: Routes = [
  { 
    path: '',
    children: [
      { path: 'configuración/datos',
        component: MyConfigurationComponent,
        title: 'Mis datos',
        canDeactivate: [PreventUnsavedChangesWorkspace],
      },
      {
        path: 'mis-pacientes',
        component: ClientsComponent,
        title: 'Mis pacientes'
      },
      {
        path: 'mis-pacientes/registrar',
        component: UpsertClientComponent,
        title: 'Registrar paciente',
        canDeactivate: [PreventUnsavedChangesAdmin]
      },
      {
        path: 'mis-pacientes/actualizar/:id',
        component: UpsertClientComponent,
        title: 'Actualizar paciente'
      },
      {
        path: 'mis-pacientes/ver/:id',
        component: ShowInvoiceComponent,
        title: 'Actualizar paciente'
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
        path: 'facturas/registrar',
        component: RegisterInvoiceComponent,
        canActivate: [PermissionGuard],
        canDeactivate: [PreventUnsavedChangesWorkspace],
        title: 'Registrar Factura',
        data: { permissions: [ Permission.CreateInvoices ] }
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
        path: 'facturas/ver/:id',
        component: ShowInvoiceComponent,
        title: 'Actualizar paciente'
      }
    ]
  }
]
