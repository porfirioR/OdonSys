import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { AuthApiModel } from '../../models/users/api/auth-api-model';
import { LoginRequest } from '../../models/users/api/login-request';
import { RegisterUserRequest } from '../../models/users/api/register-user-request';
import { UserInfoService } from '../shared/user-info.service';
import { RoleApiService } from './role-api.service';

@Injectable({
  providedIn: 'root'
})
export class AuthApiService {
  private baseUrl = `${environment.apiUrl}/authentication`;

  constructor(
    private readonly http: HttpClient,
    private readonly userInfoService: UserInfoService,
    private readonly roleApiService: RoleApiService
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
    }
    const login$ = this.http.post<AuthApiModel>(`${this.baseUrl}/login`, null, httpOptions)
    return login$.pipe(
      switchMap(authApiModel => {
        this.userInfoService.setUserLogin(authApiModel)
        return this.roleApiService.getMyPermissions().pipe(map(permissions => {
          this.userInfoService.setUserPermissions(permissions)
          return authApiModel
        }))
      })
    )
  }

  public register = (request: RegisterUserRequest): Observable<AuthApiModel> => {
    return this.http.post<AuthApiModel>(`${this.baseUrl}/register`, request).pipe(
      switchMap(authApiModel => {
        this.userInfoService.setUserLogin(authApiModel)
        return this.roleApiService.getMyPermissions().pipe(map(permissions => {
          this.userInfoService.setUserPermissions(permissions)
          return authApiModel
        }))
      })
    )
  }
}
