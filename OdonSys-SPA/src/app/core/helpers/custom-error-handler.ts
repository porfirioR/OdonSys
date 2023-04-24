import { ErrorHandler, Injectable, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService } from '../services/shared/alert.service';
import { HttpStatusCode } from '@angular/common/http';
import { UserInfoService } from '../services/shared/user-info.service';

@Injectable({
  providedIn: 'root'
})
export class CustomErrorHandler implements ErrorHandler {

  constructor(
    private readonly alertService: AlertService,
    private readonly router: Router,
    private readonly userInfoService: UserInfoService,
    private readonly zone: NgZone,
  ) {}

  handleError(error: any): void {
    console.error(error)
    if (error.status === 0) {
      this.router.navigate(['login'])
      return
    }
    if (typeof error === 'object' && error.error) {
      const errors = error.error;
      let message = ''
      if (errors.errors) {
        for (const key in errors.errors) {
          const element = error.error.errors[key]
          message = message.concat(`${element}\n`)
        }
      } else if(error?.status === HttpStatusCode.Unauthorized && errors && errors.title) {
        message = errors.title
        this.userInfoService.clearAllCredentials()
        this.zone.run(() => this.router.navigate(['/login']))
      } else if (errors.statusCode && errors.message) {
        message = errors.message
      } else if(errors && errors.title) {
        message = errors.title
      }
      this.alertService.showError(message)
      return
    }
  }
}
