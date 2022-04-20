import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProcedureApiModel } from '../../core/models/procedure/procedure-api-model';
import { environment } from '../../../environments/environment';
import { CreateProcedureRequest } from '../models/procedure/api/create-procedure-request';
import { UpdateProcedureRequest } from '../models/procedure/api/update-procedure-request';

@Injectable({
  providedIn: 'root'
})
export class ProcedureApiService {
  private baseUrl = `${environment.apiUrl}/procedure`;

  constructor(private http: HttpClient) { }

  public getAll = (): Observable<ProcedureApiModel[]> => {
    return this.http.get<ProcedureApiModel[]>(`${this.baseUrl}`);
  }

  public getById = (code: string): Observable<ProcedureApiModel> => {
    return this.http.get<ProcedureApiModel>(`${this.baseUrl}/${code}`);
  }

  public delete = (code: string): Observable<object> => {
    return this.http.delete(`${this.baseUrl}/${code}`);
  }

  public create = (model: CreateProcedureRequest): Observable<ProcedureApiModel> => {
    return this.http.post<ProcedureApiModel>(this.baseUrl, model);
  }

  public update = (documentList: UpdateProcedureRequest, code: string): Observable<ProcedureApiModel> => {
    return this.http.put<ProcedureApiModel>(`${this.baseUrl}/${code}`, documentList);
  }
}
