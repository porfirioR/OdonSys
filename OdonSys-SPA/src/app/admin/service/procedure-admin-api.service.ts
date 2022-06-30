import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProcedureApiModel } from '../../core/models/procedure/procedure-api-model';
import { environment } from '../../../environments/environment';
import { CreateProcedureRequest } from '../models/procedure/api/create-procedure-request';
import { UpdateProcedureRequest } from '../models/procedure/api/update-procedure-request';
import { BasicServiceModule } from 'src/app/basic-service.module';

@Injectable({
  providedIn: BasicServiceModule
})
export class ProcedureApiService {
  private baseUrl = `${environment.apiUrl}/procedure`;

  constructor(private http: HttpClient) { }

  public getAll = (): Observable<ProcedureApiModel[]> => {
    return this.http.get<ProcedureApiModel[]>(`${this.baseUrl}`);
  }

  public getById = (id: string, active: boolean): Observable<ProcedureApiModel> => {
    return this.http.get<ProcedureApiModel>(`${this.baseUrl}/${id}/${active}`);
  }

  public delete = (id: string): Observable<object> => {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  public create = (model: CreateProcedureRequest): Observable<ProcedureApiModel> => {
    return this.http.post<ProcedureApiModel>(this.baseUrl, model);
  }

  public update = (model: UpdateProcedureRequest): Observable<ProcedureApiModel> => {
    return this.http.put<ProcedureApiModel>(`${this.baseUrl}`, model);
  }
}
