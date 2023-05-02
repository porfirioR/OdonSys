import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DoctorApiModel } from '../../core/models/api/doctor/doctor-api-model';
import { PatchRequest } from '../../core/models/api/patch-request';
import { UserRoleApiRequest } from '../../core/models/api/roles/user-role-api-request';
import { BasicServiceModule } from '../../basic-service.module';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: BasicServiceModule
})
export class UserApiService {
  private baseUrl = `${environment.apiUrl}/users`

  constructor(
    private readonly http: HttpClient
  ) {}

  public approve = (id: string): Observable<DoctorApiModel> => {
    return this.http.post<DoctorApiModel>(`${this.baseUrl}/approve/${id}`, null)
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
