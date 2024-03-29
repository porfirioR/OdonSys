import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { DoctorApiModel } from '../../models/api/doctor/doctor-api-model';
import { UpdateUserRequest } from '../../models/users/api/update-user-request';
import { AssignClientRequest } from '../../models/api/clients/assign-client-request';
import { ClientApiModel } from '../../models/api/clients/client-api-model';

@Injectable({
  providedIn: 'root'
})
export class DoctorApiService {
  private baseUrl = `${environment.apiUrl}/doctors`

  constructor(private readonly http: HttpClient) { }

  public update = (model: UpdateUserRequest): Observable<DoctorApiModel> => {
    return this.http.put<DoctorApiModel>(`${this.baseUrl}`, model)
  }

  public getById = (id: string): Observable<DoctorApiModel> => {
    return this.http.get<DoctorApiModel>(`${this.baseUrl}/${id}`)
  }

  public assignClientToUser = (request: AssignClientRequest): Observable<ClientApiModel> => {
    return this.http.post<ClientApiModel>(`${this.baseUrl}`, request)
  }

}
