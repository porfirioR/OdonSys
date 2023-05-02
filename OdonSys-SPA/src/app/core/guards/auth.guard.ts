import { Injectable, NgZone } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';
import { UserInfoService } from '../services/shared/user-info.service';
import { AlertService } from '../services/shared/alert.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private userInfoService: UserInfoService,
    private readonly router: Router,
    private readonly alertService: AlertService,
    private readonly zone: NgZone,
  ) { }

  canActivate(): boolean | UrlTree {
    const user = this.userInfoService.getUserData()
    const token = this.userInfoService.getToken()
    const permissions = this.userInfoService.getPermissions()
    const isValidUser = user && user.active && user.approved
    // todo expired token
    if (token && isValidUser && permissions.length > 0) {
      return true
    }
    if(permissions.length === 0) {
      this.alertService.showInfo('No tiene permisos para acceder al sistema, favor contacte con el administrador, e intente devuelta')
    }
    return this.zone.run(() => this.router.createUrlTree(['/login']))
  }
}
