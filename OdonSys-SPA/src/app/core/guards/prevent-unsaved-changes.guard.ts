import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AlertService } from '../services/shared/alert.service';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {

  constructor(protected readonly alertService: AlertService) { }

  canDeactivate(component: unknown): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return true
  }

  protected showQuestionModal = async (): Promise<boolean> => {
    const result = await this.alertService.showQuestionModal(
      'Salir sin guardar',
      '¿Estás seguro de que quieres continuar? Cualquier cambio hecho no sera guardado.',
      'question'
    );
    return result.value ?? false;
  }
}
