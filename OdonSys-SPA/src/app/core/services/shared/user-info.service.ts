import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { UserDataApiModel } from '../../models/api/user-data-api-model';
import { AuthApiModel } from '../../models/users/api/auth-api-model';
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

  public getUserId = (): string => {
    const userData = this.getUserData();
    return userData.id;
  }

  public getToken = (): string => {
    return this.localStorageService.getByKey(this.userToken);
  }

  public setUserLogin = (auth: AuthApiModel): void => {
    this.localStorageService.clearAll(this.userKey);
    this.localStorageService.clearAll(this.userToken);
    this.localStorageService.setData(this.userToken, auth.token);
    this.localStorageService.setData(this.userKey, JSON.stringify(auth.user));
    //nextStep add this.userDataApiService.getUserData() for roles, and other things
  }

  private getUserData = (): UserDataApiModel => {
    const userData = this.localStorageService.getByKey(this.userKey);
    // if (!userData) {
    //   throw new Error('Unable to get UserInfo');
    // }
    return userData;
  }
}
