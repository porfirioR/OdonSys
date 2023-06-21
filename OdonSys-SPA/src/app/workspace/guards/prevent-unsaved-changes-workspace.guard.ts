import { inject } from '@angular/core';
import { CanDeactivateFn } from '@angular/router';
import { RegisterInvoiceComponent } from '../components/register-invoice/register-invoice.component';
import { MyConfigurationComponent } from '../components/my-configuration/my-configuration.component';
import { AlertService } from '../../core/services/shared/alert.service';
import { UpsertClientComponent } from '../../core/components/upsert-client/upsert-client.component';

export const PreventUnsavedChangesWorkspace: CanDeactivateFn<RegisterInvoiceComponent | MyConfigurationComponent> = (component: RegisterInvoiceComponent | MyConfigurationComponent | UpsertClientComponent) => {
  const alertService = inject(AlertService)
  if (component.saving) {
    return true
  }
  return component.formGroup.dirty ?
  alertService.showQuestionModal(
    'Salir sin guardar',
    '¿Estás seguro de que quieres continuar? Cualquier cambio hecho no sera guardado.',
    'question'
  ).then(result => result.value ?? false) :
  true
}
