import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { AuthApiModel } from '../../models/users/api/auth-api-model';
import { LoginRequest } from '../../models/users/api/login-request';
import { RegisterUserRequest } from '../../models/users/api/register-user-request';
import { UserInfoService } from '../shared/user-info.service';
import { Store } from '@ngrx/store';
import * as userInfoActions from '../../store/user-info/user-info.actions';

@Injectable({
  providedIn: 'root'
})
export class AuthApiService {
  private baseUrl = `${environment.apiUrl}/authentication`;

  constructor(
    private readonly http: HttpClient,
    private readonly userInfoService: UserInfoService,
    private store: Store
  ) { }

  public login = (request: LoginRequest): Observable<AuthApiModel> => {
    const auth = btoa(`${request.email}:${request.password}`)
    const httpOptions = {
      headers: new HttpHeaders(
        {
          'Content-Type': 'application/json',
          'Authorization': `Basic ${auth}`,
        }
      )
    };
    return this.http.post<AuthApiModel>(`${this.baseUrl}/login`, null, httpOptions).pipe(
      switchMap(x => {
        this.store.dispatch(userInfoActions.getUserInfo())
        // this.userInfoService.setUserLogin(x);
        return of(x);
      }
    ));
  }

  public register = (request: RegisterUserRequest): Observable<AuthApiModel> => {
    return this.http.post<AuthApiModel>(`${this.baseUrl}/register`, request).pipe(
      switchMap(x => {
        this.userInfoService.setUserLogin(x);
        return of(x);
      }
    ));
  }
}
