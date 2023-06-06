import { Injectable, NgZone } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { UserInfoService } from '../services/shared/user-info.service';

@Injectable({
  providedIn: 'root'
})
export class PublicGuard implements CanActivate {

  constructor(
    private router: Router,
    private readonly userInfoService: UserInfoService,
    private readonly zone: NgZone,
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const publicRouters = [
      'registrar',
      'login'
    ]
    const token = this.userInfoService.getToken()
    const hasTokenExpired = this.userInfoService.hasUserTokenExpired()
    if (token && !hasTokenExpired && state.url.includes('autenticacion/ingresar')) {
      return this.zone.run(() => this.router.createUrlTree(['/inicio']))
    }
    return publicRouters.some(x => state.url.includes(x))
  }
}


