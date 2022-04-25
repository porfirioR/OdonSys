import { ErrorHandler, Injectable } from '@angular/core';
import { AlertService } from '../services/shared/alert.service';

@Injectable({
  providedIn: 'root'
})
export class CustomErrorHandler implements ErrorHandler {

  constructor(private readonly alertService: AlertService) {}

  handleError(error: any): void {
    console.error(error);

    if (typeof error === 'string') {
      this.alertService.showError(error);
      return;
    }
  }
}
