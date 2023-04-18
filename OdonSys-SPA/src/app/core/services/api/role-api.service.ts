import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { RoleApiModel } from '../../models/api/roles/role-api-model';
import { UpdateRoleApiRequest } from '../../models/api/roles/update-role-api-request';
import { CreateRoleApiRequest } from '../../models/api/roles/create-role-api-request';
import { PermissionModel } from '../../models/view/permission-model';

@Injectable({
  providedIn: 'root'
})
export class RoleApiService {
  private baseUrl = `${environment.apiUrl}/roles`;

  constructor(private http: HttpClient) { }

  public getAll = (): Observable<RoleApiModel[]> => {
    return this.http.get<RoleApiModel[]>(`${this.baseUrl}`);
  }

  public getMyPermissions = (): Observable<string[]> => {
    return this.http.get<string[]>(`${this.baseUrl}/permissions-role`);
  }

  public getPermissions = (): Observable<PermissionModel[]> => {
    return this.http.get<PermissionModel[]>(`${this.baseUrl}/permissions`);
  }

  public create = (request: CreateRoleApiRequest): Observable<RoleApiModel> => {
    return this.http.post<RoleApiModel>(`${this.baseUrl}`, request);
  }
  public update = (model: UpdateRoleApiRequest): Observable<RoleApiModel> => {
    return this.http.put<RoleApiModel>(`${this.baseUrl}`, model);
  }

  public getByCode = (code: string): Observable<RoleApiModel> => {
    return this.http.get<RoleApiModel>(`${this.baseUrl}/${code}`);
  }
}
