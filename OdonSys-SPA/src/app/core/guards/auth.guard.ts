import { Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';
import { UserInfoService } from '../services/shared/user-info.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private userInfoService: UserInfoService, private readonly router: Router) { }

  canActivate(): boolean | UrlTree {
    return this.userInfoService.getUserData() ? true : this.router.createUrlTree(['/login']);
  }
}
