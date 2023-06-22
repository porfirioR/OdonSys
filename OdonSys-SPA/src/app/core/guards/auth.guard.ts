import { NgZone, inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { UserInfoService } from '../services/shared/user-info.service';
import { AlertService } from '../services/shared/alert.service';

export const AuthGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree  => {
  const userInfoService = inject(UserInfoService)
  const router = inject(Router)
  const alertService = inject(AlertService)
  const zone = inject(NgZone)

  const user = userInfoService.getUserData()
  const token = userInfoService.getToken()
  const permissions = userInfoService.getPermissions()
  const hasTokenExpired = userInfoService.hasUserTokenExpired()
  const isValidUser = user && user.active && user.approved
  if (token && isValidUser && permissions.length > 0 && !hasTokenExpired) {
    return true
  }
  if(permissions.length === 0) {
    alertService.showInfo('No tiene permisos para acceder al sistema, favor contacte con el administrador, e intente devuelta')
  }
  return zone.run(() => router.createUrlTree(['/login']))
}
