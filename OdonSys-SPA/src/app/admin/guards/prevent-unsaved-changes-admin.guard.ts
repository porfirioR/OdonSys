import { inject } from '@angular/core';
import { CanDeactivateFn } from '@angular/router';
import { UpsertClientComponent } from '../../core/components/upsert-client/upsert-client.component';
import { UpsertProcedureComponent } from '../components/upsert-procedure/upsert-procedure.component';
import { UpsertRoleComponent } from '../components/upsert-role/upsert-role.component';
import { AlertService } from '../../core/services/shared/alert.service';

export const PreventUnsavedChangesAdmin: CanDeactivateFn<UpsertRoleComponent | UpsertProcedureComponent | UpsertClientComponent>  = (component: UpsertRoleComponent | UpsertProcedureComponent | UpsertClientComponent): boolean | Promise<boolean> => {
  const alertService = inject(AlertService)

  if (component.ignorePreventUnsavedChanges) {
    return true
  }
  if (component.formGroup.dirty) {
    return alertService.showQuestionModal(
      'Salir sin guardar',
      '¿Estás seguro de que quieres continuar? Cualquier cambio hecho no sera guardado.',
      'question'
    ).then(result => result.value ?? false)
  }
  return  true
}


