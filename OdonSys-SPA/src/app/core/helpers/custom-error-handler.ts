import { ErrorHandler, Injectable } from '@angular/core';
import { AlertService } from '../services/shared/alert.service';

@Injectable({
  providedIn: 'root'
})
export class CustomErrorHandler implements ErrorHandler {

  constructor(private readonly alertService: AlertService) {}

  handleError(error: any): void {
    console.error(error);

    if (typeof error === 'object' && error.error) {
      const errors = error.error;
      let message = '';
      for (const key in errors.errors) {
        const element = error.error.errors[key];
        message = message.concat(`${element}\n`)
      }
      this.alertService.showError(message);
      return;
    }
  }
}
