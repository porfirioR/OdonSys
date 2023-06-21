import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { RegisterInvoiceComponent } from '../components/register-invoice/register-invoice.component';
import { MyConfigurationComponent } from '../components/my-configuration/my-configuration.component';
import { AlertService } from '../../core/services/shared/alert.service';
import { PreventUnsavedChangesGuard } from '../../core/guards/prevent-unsaved-changes.guard';
import { UpsertClientComponent } from '../../core/components/upsert-client/upsert-client.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesWorkspace extends PreventUnsavedChangesGuard implements CanDeactivate<RegisterInvoiceComponent | MyConfigurationComponent> {

  constructor(protected readonly alertService: AlertService) {
    super(alertService)
  }

  canDeactivate(component: RegisterInvoiceComponent | MyConfigurationComponent | UpsertClientComponent): boolean | Promise<boolean> {
    if (component.saving) {
      return true
    }
    return component.formGroup.dirty ? this.showQuestionModal() : true
  }
}
