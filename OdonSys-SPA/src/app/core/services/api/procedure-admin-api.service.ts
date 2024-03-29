import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { CreateProcedureRequest } from '../../models/procedure/create-procedure-request';
import { ProcedureApiModel } from '../../models/procedure/procedure-api-model';
import { UpdateProcedureRequest } from '../../models/procedure/update-procedure-request';
import { PatchRequest } from '../../models/api/patch-request';

@Injectable({
  providedIn: 'root'
})
export class ProcedureApiService {
  private baseUrl = `${environment.apiUrl}/procedure`

  constructor(private http: HttpClient) { }

  public getAll = (): Observable<ProcedureApiModel[]> => {
    return this.http.get<ProcedureApiModel[]>(`${this.baseUrl}`)
  }

  public getById = (id: string, active: boolean): Observable<ProcedureApiModel> => {
    return this.http.get<ProcedureApiModel>(`${this.baseUrl}/${id}/${active}`)
  }

  public delete = (id: string): Observable<object> => {
    return this.http.delete(`${this.baseUrl}/${id}`)
  }

  public create = (model: CreateProcedureRequest): Observable<ProcedureApiModel> => {
    return this.http.post<ProcedureApiModel>(this.baseUrl, model)
  }

  public update = (model: UpdateProcedureRequest): Observable<ProcedureApiModel> => {
    return this.http.put<ProcedureApiModel>(`${this.baseUrl}`, model)
  }

  public changeVisibility = (id: string, request: PatchRequest): Observable<ProcedureApiModel> => {
    return this.http.patch<ProcedureApiModel>(`${this.baseUrl}/${id}`, [request])
  }
}
