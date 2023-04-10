import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DoctorApiModel } from '../../core/models/api/doctor/doctor-api-model';
import { environment } from '../../../environments/environment';
import { BasicServiceModule } from '../../../app/basic-service.module';
import { PatchRequest } from '../../core/models/api/patch-request';

@Injectable({
  providedIn: BasicServiceModule
})
export class UserApiService {
  private baseUrl = `${environment.apiUrl}/users`;

  constructor(private readonly http: HttpClient) {}

  public approve = (id: string): Observable<DoctorApiModel> => {
    return this.http.post<DoctorApiModel>(`${this.baseUrl}/approve/${id}`, null);
  };

  public getAll = (): Observable<DoctorApiModel[]> => {
    return this.http.get<DoctorApiModel[]>(`${this.baseUrl}`);
  };

  public doctorVisibility = (id: string, request: PatchRequest): Observable<DoctorApiModel> => {
    return this.http.patch<DoctorApiModel>(`${this.baseUrl}/${id}`, [request]);
  };

}
