import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { UpsertRoleComponent } from '../components/roles/upsert-role/upsert-role.component';
import { UpsertProcedureComponent } from '../components/admin-procedure/upsert-procedure/upsert-procedure.component';
import { AlertService } from '../../core/services/shared/alert.service';
import { UpsertClientComponent } from '../../core/components/clients/upsert-client/upsert-client.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChanges implements CanDeactivate<UpsertRoleComponent | UpsertProcedureComponent | UpsertClientComponent> {
  constructor(private readonly alertService: AlertService) { }

  canDeactivate(component: UpsertRoleComponent | UpsertProcedureComponent | UpsertClientComponent): boolean | Promise<boolean> {
    if (component.saveData) {
      return true
    } else if (component.formGroup.dirty) {
      return this.alertService.showQuestionModal(
        'Salir sin guardar',
        '¿Estás seguro de que quieres continuar? Cualquier cambio no sera guardado.',
        'question'
      ).then((result) => result.value ?? false)
    }
    return true
  }

}