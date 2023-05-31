import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { UpsertClientComponent } from '../../core/components/upsert-client/upsert-client.component';
import { UpsertProcedureComponent } from '../components/upsert-procedure/upsert-procedure.component';
import { UpsertRoleComponent } from '../components/upsert-role/upsert-role.component';
import { AlertService } from '../../core/services/shared/alert.service';
import { PreventUnsavedChangesGuard } from '../../core/guards/prevent-unsaved-changes.guard';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesAdmin extends PreventUnsavedChangesGuard implements CanDeactivate<UpsertRoleComponent | UpsertProcedureComponent | UpsertClientComponent> {
  constructor(protected readonly alertService: AlertService) {
    super(alertService)
  }

  canDeactivate(component: UpsertRoleComponent | UpsertProcedureComponent | UpsertClientComponent): boolean | Promise<boolean> {
    if (component.ignorePreventUnsavedChanges) {
      return true
    }
    return component.formGroup.dirty ? this.showQuestionModal() : true
  }

}
