import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { OrthodonticApiModel } from '../../models/api/orthodontics/orthodontic-api-model';
import { OrthodonticClientRequest } from '../../models/api/orthodontics/orthodontic-request';

@Injectable({
  providedIn: 'root'
})
export class OrthodonticApiService {
  private baseUrl = `${environment.apiUrl}/orthodontics`

  constructor(
    private readonly http: HttpClient
  ) {}

  public create = (request: OrthodonticClientRequest): Observable<OrthodonticApiModel> => {
    return this.http.post<OrthodonticApiModel>(this.baseUrl, request)
  }

  public update = (id: string, request: OrthodonticClientRequest): Observable<OrthodonticApiModel> => {
    return this.http.put<OrthodonticApiModel>(`${this.baseUrl}/${id}`, request)
  }

  public getAll = (): Observable<OrthodonticApiModel[]> => {
    return this.http.get<OrthodonticApiModel[]>(this.baseUrl)
  }

  public getPatientOrthodonticsById = (id: string): Observable<OrthodonticApiModel[]> => {
    return this.http.get<OrthodonticApiModel[]>(`${this.baseUrl}/patient-orthodontics/${id}`)
  }

  public delete = (id: string): Observable<OrthodonticApiModel> => {
    return this.http.delete<OrthodonticApiModel>(`${this.baseUrl}/${id}`)
  }

}
