import { Injectable } from '@angular/core';
import { AuthApiModel } from '../../models/users/api/auth-api-model';
import { UserApiModel } from '../../models/users/api/user-api-model';
import { UserDataApiService } from '../api/user-data-api.service';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {
  private userKey = 'userData';
  private userToken = 'token';
  private permissionKey = 'permissions';

  constructor(private readonly localStorageService: LocalStorageService, private readonly userDataApiService: UserDataApiService) { }

  // public loadUserData = (): Observable<string> => {
  //   this.localStorageService.clearAll(this.userKey)
  //   this.localStorageService.clearAll(this.userToken)
  //   return this.userDataApiService.getUserData().pipe(
  //     switchMap((userInfo: UserDataApiModel) => {
  //       this.localStorageService.setData(this.userKey, userInfo)
  //       return of(userInfo.name)
  //     }),
  //     catchError((error) => {
  //       return throwError(`Problemas al traer datos del usuario. ${JSON.stringify(error)}`)
  //     })
  //   )
  // }

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
    const userData: UserApiModel = this.localStorageService.getByKey(this.userKey)
    return userData
  }

  public getPermissions = (): string[] => {
    const permissions: string[] = this.localStorageService.getArrayByKey(this.userKey)
    return permissions
  }


}
