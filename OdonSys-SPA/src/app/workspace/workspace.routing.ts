import { Routes } from '@angular/router';
import { ClientsComponent } from '../core/components/clients/clients.component';
import { UpsertClientComponent } from '../core/components/upsert-client/upsert-client.component';
import { MyConfigurationComponent } from './components/my-configuration/my-configuration.component';
import { InvoicesComponent } from './components/invoices/invoices.component';
import { UpsertInvoiceComponent } from './components/upsert-invoice/upsert-invoice.component';
import { PermissionGuard } from '../core/guards/permission.guard';
import { Permission } from '../core/enums/permission.enum';
import { PreventUnsavedChangesWorkspace } from './guards/prevent-unsaved-changes-workspace.guard';
import { MyInvoiceComponent } from './components/my-invoice/my-invoice.component';

export const WorkspaceRoutes: Routes = [
  { 
    path: '',
    children: [
      { path: 'configuración/datos', component: MyConfigurationComponent, title: 'Mis datos' },
      { path: 'mis-pacientes', component: ClientsComponent, title: 'Mis pacientes' },
      { path: 'mis-pacientes/registrar', component: UpsertClientComponent, title: 'Registrar paciente' },
      { path: 'mis-pacientes/actualizar/:id', component: UpsertClientComponent, title: 'Actualizar paciente' },
      {
        path: 'facturas',
        component: InvoicesComponent,
        canActivate: [PermissionGuard],
        title: 'Facturas',
        data: { permissions: [ Permission.AccessInvoices ] }
      },
      {
        path: 'mis-facturas',
        component: MyInvoiceComponent,
        canActivate: [PermissionGuard],
        title: 'Mis Facturas',
        data: { permissions: [ Permission.AccessMyInvoices ] }
      },
      {
        path: 'facturas/registrar',
        component: UpsertInvoiceComponent,
        canActivate: [PermissionGuard],
        canDeactivate: [PreventUnsavedChangesWorkspace],
        title: 'Registrar Factura',
        data: { permissions: [ Permission.CreateInvoices ] }
      },
      {
        path: 'mis-facturas/registrar',
        component: UpsertInvoiceComponent,
        canActivate: [PermissionGuard],
        canDeactivate: [PreventUnsavedChangesWorkspace],
        title: 'Registrar Factura',
        data: { permissions: [ Permission.CreateInvoices ] }
      }
    ]
  }
];
