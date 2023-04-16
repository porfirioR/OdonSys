import { Injectable } from '@angular/core';
import { AuthApiModel } from '../../models/users/api/auth-api-model';
import { UserApiModel } from '../../models/users/api/user-api-model';
import { LocalStorageService } from './local-storage.service';
import { Permission } from '../../enums/permission.enum';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {
  private userKey = 'userData';
  private userToken = 'token';
  private permissionKey = 'permissions';

  constructor(
    private readonly localStorageService: LocalStorageService
  ) { }

  public getToken = (): string => {
    return this.localStorageService.getByKey(this.userToken)
  }

  public setUserLogin = (auth: AuthApiModel): void => {
    this.localStorageService.setData(this.userToken, auth.token)
    this.localStorageService.setData(this.userKey, JSON.stringify(auth.user))
  }

  public setUserPermissions = (permissions: string[]): void => {
    this.localStorageService.setData(this.permissionKey, permissions)
  }

  public clearAll = () => {
    this.localStorageService.clearAll(this.userKey)
    this.localStorageService.clearAll(this.userToken)
    this.localStorageService.clearAll(this.permissionKey)
  }

  public getUserData = (): UserApiModel => {
    const userData = JSON.parse(this.localStorageService.getByKey(this.userKey)) as UserApiModel;
    return userData;
  }

  public getPermissions = (): string[] => {
    const permissions: string[] = this.localStorageService.getArrayByKey(this.permissionKey)
    return permissions
  }

  public havePermission = (permission: Permission): boolean => this.havePermissions([permission])

  public havePermissions = (pagePermissions: Permission[]): boolean => {
    const permissions = this.getPermissions()
    return pagePermissions.every(x => permissions.includes(x))
  }
}
