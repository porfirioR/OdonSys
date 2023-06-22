import { NgZone, inject } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanActivateFn } from '@angular/router';
import { UserInfoService } from '../services/shared/user-info.service';

export const PublicGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const userInfoService = inject(UserInfoService)
  const router = inject(Router)
  const zone = inject(NgZone)

  const publicRouters = [
    'registrar',
    'login'
  ]
  const token = userInfoService.getToken()
  const hasTokenExpired = userInfoService.hasUserTokenExpired()
  if (token && !hasTokenExpired && publicRouters.some(x => state.url.includes(x))) {
    return zone.run(() => router.createUrlTree(['/inicio']))
  }
  return true
}


