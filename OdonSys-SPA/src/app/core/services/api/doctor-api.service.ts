import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { DoctorApiModel } from '../../models/api/doctor/doctor-api-model';
import { UpdateUserApiModel } from '../../models/users/update-user-api-model';
import { UpdateUserRequest } from '../../models/users/update-user-request';

@Injectable({
  providedIn: 'root'
})
export class DoctorApiService {
  private baseUrl = `${environment.apiUrl}/doctors`;

  constructor(private readonly http: HttpClient) { }

  // public getById = (id: string, active: boolean): Observable<ProcedureApiModel> => {
  //   return this.http.get<ProcedureApiModel>(`${this.baseUrl}/${id}/${active}`);
  // }

  public updateConfiguration = (id: string, model: UpdateUserRequest): Observable<UpdateUserApiModel> => {
    return this.http.put<UpdateUserApiModel>(`${this.baseUrl}`, model);
  }

  
  public getAll = (reference: boolean = false): Observable<DoctorApiModel[]> => {
    return this.http.get<DoctorApiModel[]>(`${this.baseUrl}/${reference}`);
  }

  public delete = (id: string): Observable<object> => {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  public approve = (id: string): Observable<DoctorApiModel> => {
    return this.http.put<DoctorApiModel>(`${this.baseUrl}/${id}`, null);
  }

}
