import { ErrorHandler, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService } from '../services/shared/alert.service';

@Injectable({
  providedIn: 'root'
})
export class CustomErrorHandler implements ErrorHandler {

  constructor(
    private readonly alertService: AlertService,
    private readonly router: Router
  ) {}

  handleError(error: any): void {
    console.error(error);

    if (typeof error === 'object' && error.error) {
      const errors = error.error;
      let message = '';
      if (errors.errors) {
        for (const key in errors.errors) {
          const element = error.error.errors[key];
          message = message.concat(`${element}\n`)
        }
      } else if (errors.statusCode) {
        message = errors.message;
      }
      this.alertService.showError(message);
      return;
    } else if (error.status === 401) {
      this.router.navigate(['login']);
    }
  }
}
