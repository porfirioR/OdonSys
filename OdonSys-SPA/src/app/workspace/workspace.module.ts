import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { WorkspaceRoutes } from './workspace.routing';
import { MyConfigurationComponent } from './components/my-configuration/my-configuration.component';
import { InvoicesComponent } from './components/invoices/invoices.component';
import { RegisterInvoiceComponent } from './components/register-invoice/register-invoice.component';
import { ShowInvoiceComponent } from './components/show-invoice/show-invoice.component';
import { PaymentModalComponent } from './modals/payment-modal/payment-modal.component';
import { UpdateInvoiceComponent } from './components/update-invoice/update-invoice.component';
import { OrthodonticsComponent } from './components/orthodontics/orthodontics.component';
import { UpsertOrthodonticComponent } from './components/upsert-orthodontic/upsert-orthodontic.component';

@NgModule({
  imports: [
    CoreModule,
    CommonModule,
    RouterModule.forChild(WorkspaceRoutes)
  ],
  declarations: [
    MyConfigurationComponent,
    InvoicesComponent,
    RegisterInvoiceComponent,
    PaymentModalComponent,
    ShowInvoiceComponent,
    UpdateInvoiceComponent,
    OrthodonticsComponent,
    UpsertOrthodonticComponent
  ]
})
export class WorkspaceModule { }
