import { Injectable } from '@angular/core';
import { AuthApiModel } from '../../models/users/api/auth-api-model';
import { UserApiModel } from '../../models/users/api/user-api-model';
import { LocalStorageService } from './local-storage.service';
import { Permission } from '../../enums/permission.enum';
import { SubscriptionService } from './subscription.service';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {
  private userKey = 'userData'
  private tokenExpirationKey = 'tokenExpiration'
  private userToken = 'token'
  private permissionKey = 'permissions'

  constructor(
    private readonly localStorageService: LocalStorageService,
    private readonly subscriptionService: SubscriptionService
  ) { }

  public getToken = (): string => {
    return this.localStorageService.getByKey(this.userToken, false)
  }

  public setUserLogin = (auth: AuthApiModel): void => {
    this.localStorageService.setData(this.userToken, auth.token, true)
    this.localStorageService.setData(this.tokenExpirationKey, auth.expirationDate, true)
    this.localStorageService.setData(this.userKey, JSON.stringify(auth.user))
  }

  public setUser = (user: UserApiModel): void => {
    this.localStorageService.setData(this.userKey, JSON.stringify(user))
  }

  public setRoles = (roles: string[]) => {
    const user = this.getUserData()
    user.roles = roles
    this.localStorageService.setData(this.userKey, JSON.stringify(user))
  }

  public setUserPermissions = (permissions: string[]): void => {
    this.localStorageService.clearAll(this.permissionKey)
    this.localStorageService.setData(this.permissionKey, permissions)
    this.subscriptionService.emitCheckMenu(true)
  }

  public clearAllCredentials = () => {
    this.localStorageService.clearAll(this.userKey)
    this.localStorageService.clearAll(this.userToken)
    this.localStorageService.clearAll(this.permissionKey)
    this.localStorageService.clearAll(this.tokenExpirationKey)
  }

  public clearToken = (): void => {
    this.localStorageService.clearAll(this.userToken)
  }

  public getUserData = (): UserApiModel => {
    const userData: UserApiModel = JSON.parse(this.localStorageService.getByKey(this.userKey))
    return userData
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

  public hasUserTokenExpired = (): boolean => {
    const tokenExpiration = this.localStorageService.getByKey(this.tokenExpirationKey, false)
    if (!tokenExpiration) {
      return true
    }
    const expirationDate = new Date(tokenExpiration)
    return expirationDate <= new Date()
  }
}
