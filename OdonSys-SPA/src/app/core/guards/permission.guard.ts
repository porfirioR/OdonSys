import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot, UrlTree } from '@angular/router';
import { UserInfoService } from '../services/shared/user-info.service';
import { AlertService } from '../services/shared/alert.service';
import { Permission } from '../enums/permission.enum';

// eslint-disable-next-line @typescript-eslint/no-unused-vars
export const PermissionGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree => {
  const userInfoService = inject(UserInfoService)
  const alertService = inject(AlertService)

  const routePermissions: Permission[] = route.data['permissions']
  const permissions = userInfoService.getPermissions()
  const invalidPermission = routePermissions.filter(x => !permissions.includes(x))
  if (invalidPermission.length > 0) {
    alertService.showError(`No tienes los permisos necesarios para acceder a esta página`)
    return false
  }
  return true
}
