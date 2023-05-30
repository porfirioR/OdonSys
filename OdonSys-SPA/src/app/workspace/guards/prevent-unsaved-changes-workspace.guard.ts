import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot } from '@angular/router';
import { AlertService } from '../../core/services/shared/alert.service';
import { RegisterInvoiceComponent } from '../components/register-invoice/register-invoice.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesWorkspace implements CanDeactivate<RegisterInvoiceComponent> {
  constructor(private readonly alertService: AlertService) { }

  canDeactivate(component: RegisterInvoiceComponent,
    currentRoute: ActivatedRouteSnapshot, 
    currentState: RouterStateSnapshot): boolean | Promise<boolean> {
      console.log(currentRoute);
      console.log(currentState);
      
    if (component.formGroup.dirty) {
      return this.alertService.showQuestionModal(
        'Salir sin guardar',
        '¿Estás seguro de que quieres continuar? Cualquier cambio hecho no sera guardado.',
        'question'
      ).then((result) => result.value ?? false)
    }
    return true
  }
}
