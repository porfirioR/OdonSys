import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DoctorApiModel } from '../../models/api/doctor/doctor-api-model';
import { PatchRequest } from '../../models/api/patch-request';
import { UserRoleApiRequest } from '../../models/api/roles/user-role-api-request';
import { UserApiModel } from '../../models/users/api/user-api-model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserApiService {
  private baseUrl = `${environment.apiUrl}/users`

  constructor(
    private readonly http: HttpClient
  ) {}

  public approve = (id: string): Observable<UserApiModel> => {
    return this.http.post<UserApiModel>(`${this.baseUrl}/approve/${id}`, null)
  }

  public getAll = (): Observable<DoctorApiModel[]> => {
    return this.http.get<DoctorApiModel[]>(`${this.baseUrl}`)
  }

  public changeVisibility = (id: string, request: PatchRequest): Observable<DoctorApiModel> => {
    return this.http.patch<DoctorApiModel>(`${this.baseUrl}/${id}`, [request])
  }

  public setUserRoles = (request: UserRoleApiRequest): Observable<string[]> => {
    return this.http.post<string[]>(`${this.baseUrl}/user-roles`, request)
  }

}
