import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { UserInfoService } from '../services/shared/user-info.service';
import { Permission } from '../enums/permission.enum';
import { AlertService } from '../services/shared/alert.service';

@Injectable({
  providedIn: 'root'
})
export class PermissionGuard implements CanActivate {
  constructor(
    private readonly userInfoService: UserInfoService,
    private readonly alertService: AlertService
  ) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const routePermissions: Permission[] = route.data['permissions']
    const permissions = this.userInfoService.getPermissions()
    const invalidPermission = routePermissions.filter(x => !permissions.includes(x))
    if (invalidPermission.length > 0) {
      this.alertService.showError(`No tienes los permisos necesarios para acceder a esta página ${invalidPermission.join(',')}`)
      return false
    }
    return true
  }
  
}
