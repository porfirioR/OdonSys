import { Injectable } from '@angular/core';
import { UserDataApiModel } from '../../models/api/user-data-api-model';
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

  constructor(private readonly localStorageService: LocalStorageService, private readonly userDataApiService: UserDataApiService) { }

  // public loadUserData = (): Observable<string> => {
  //   this.localStorageService.clearAll(this.userKey);
  //   this.localStorageService.clearAll(this.userToken);
  //   return this.userDataApiService.getUserData().pipe(
  //     switchMap((userInfo: UserDataApiModel) => {
  //       this.localStorageService.setData(this.userKey, userInfo);
  //       return of(userInfo.name);
  //     }),
  //     catchError((error) => {
  //       return throwError(`Problemas al traer datos del usuario. ${JSON.stringify(error)}`);
  //     })
  //   );
  // }

  public getToken = (): string => {
    return this.localStorageService.getByKey(this.userToken);
  }

  public setUserLogin = (auth: AuthApiModel): void => {
    this.localStorageService.setData(this.userToken, auth.token);
    this.localStorageService.setData(this.userKey, JSON.stringify(auth.user));
    //nextStep add this.userDataApiService.getUserData() for roles, and other things
  }

  public clearAll = () => {
    this.localStorageService.clearAll(this.userKey);
    this.localStorageService.clearAll(this.userToken);
  }

  public getUserData = (): UserApiModel => {
    const userData = JSON.parse(this.localStorageService.getByKey(this.userKey)) as UserApiModel;
    // if (!userData) {
    //   throw new Error('Unable to get UserInfo');
    // }
    return userData;
  }

}
