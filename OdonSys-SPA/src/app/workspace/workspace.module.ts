import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { WorkspaceRoutes } from './workspace.routing';
import { MyConfigurationComponent } from './components/my-configuration/my-configuration.component';
import { InvoicesComponent } from './components/invoices/invoices.component';
import { UpsertInvoiceComponent } from './components/upsert-invoice/upsert-invoice.component';

@NgModule({
  imports: [
    CoreModule,
    CommonModule,
    RouterModule.forChild(WorkspaceRoutes)
  ],
  declarations: [
    MyConfigurationComponent,
    InvoicesComponent,
    UpsertInvoiceComponent
  ]
})
export class WorkspaceModule { }
