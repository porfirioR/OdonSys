import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DoctorApiModel } from '../../core/models/api/doctor/doctor-api-model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
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

  public deactivate = (id: string): Observable<DoctorApiModel> => {
    return this.http.put<DoctorApiModel>(`${this.baseUrl}/deactivate/${id}`, null);
  };

  public activate = (id: string): Observable<DoctorApiModel> => {
    return this.http.put<DoctorApiModel>(`${this.baseUrl}/activate/${id}`, null);
  };
}
