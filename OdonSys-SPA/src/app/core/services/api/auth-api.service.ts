import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { AuthApiModel } from '../../models/users/api/auth-api-model';
import { LoginRequest } from '../../models/users/api/login-request';
// import { RegisterUserRequest } from '../../models/users/api/register-user-request';
import { LogoutApiModel } from '../../models/users/api/logout-api-model';
import { UserInfoService } from '../shared/user-info.service';
import { RoleApiService } from './role-api.service';
import { DoctorApiModel } from '../../models/api/doctor/doctor-api-model';
import { UserApiModel } from '../../models/users/api/user-api-model';

@Injectable({
  providedIn: 'root'
})
export class AuthApiService {
  private baseUrl = `${environment.apiUrl}/authentication`

  constructor(
    private readonly http: HttpClient,
    private readonly userInfoService: UserInfoService,
    private readonly roleApiService: RoleApiService
  ) { }

  public login = (request: LoginRequest): Observable<AuthApiModel> => {
    this.userInfoService.clearToken()
    const auth = btoa(`${request.email}:${request.password}`)
    const httpOptions = {
      headers: new HttpHeaders(
        {
          'Content-Type': 'application/json',
          'Authorization': `Basic ${auth}`
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

  // public register = (request: RegisterUserRequest): Observable<AuthApiModel> => {
  //   return this.http.post<AuthApiModel>(`${this.baseUrl}/register`, request)
  // }

  public registerAadB2C = (): Observable<UserApiModel> => {
    return this.http.post<UserApiModel>(`${this.baseUrl}/register-user`, null)
  }

  public getProfile = (): Observable<DoctorApiModel> => {
    return this.http.get<DoctorApiModel>(`${this.baseUrl}/profile`)
  }

  public logout = (): Observable<LogoutApiModel> => {
    return this.http.get<LogoutApiModel>(`${this.baseUrl}/logout`)
  }
}
