import { Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';
import { UserInfoService } from '../services/shared/user-info.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private userInfoService: UserInfoService,
    private readonly router: Router
  ) { }

  canActivate(): boolean | UrlTree {
    const user = this.userInfoService.getUserData()
    const token = this.userInfoService.getToken()
    const permissions = this.userInfoService.getPermissions()
    const isValidUser = user && user.active && user.approved
    // todo expired token
    return token && isValidUser && permissions.length > 0 ? true : this.router.createUrlTree(['/login'])
  }
}
