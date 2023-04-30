import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BasicServiceModule } from '../../../basic-service.module';
import { ClientApiModel } from '../../models/api/clients/client-api-model';
import { PatchRequest } from '../../models/api/patch-request';
import { ClientApiService } from './client-api.service';

@Injectable({
  providedIn: BasicServiceModule
})
export class ClientAdminApiService extends ClientApiService {

  constructor(readonly http: HttpClient) {
    super(http);
  }

  public changeVisibility = (id: string, request: PatchRequest): Observable<ClientApiModel> => {
    return this.http.patch<ClientApiModel>(`${this.baseUrl}/${id}`, [request]);
  }

  public hardDelete = (id: string): Observable<ClientApiModel> => {
    return this.http.delete<ClientApiModel>(`${this.baseUrl}/${id}`);
  }

  public getById = (id: string): Observable<ClientApiModel> => {
    return this.http.get<ClientApiModel>(`${this.baseUrl}/${id}`);
  }

  public getAll = (): Observable<ClientApiModel[]> => {
    return this.http.get<ClientApiModel[]>(`${this.baseUrl}`);
  }

}
