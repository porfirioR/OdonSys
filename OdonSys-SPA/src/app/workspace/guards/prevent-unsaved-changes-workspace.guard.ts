import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { RegisterInvoiceComponent } from '../components/register-invoice/register-invoice.component';
import { MyConfigurationComponent } from '../components/my-configuration/my-configuration.component';
import { AlertService } from '../../core/services/shared/alert.service';
import { PreventUnsavedChangesGuard } from '../../core/guards/prevent-unsaved-changes.guard';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesWorkspace extends PreventUnsavedChangesGuard implements CanDeactivate<RegisterInvoiceComponent | MyConfigurationComponent> {

  constructor(protected readonly alertService: AlertService) {
    super(alertService)
  }

  canDeactivate(component: RegisterInvoiceComponent | MyConfigurationComponent): boolean | Promise<boolean> {
    return component.formGroup.dirty ? this.showQuestionModal() : true
  }
}
