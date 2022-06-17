import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClientApiModel } from '../../core/models/api/clients/client-api-model';
import { ClientSoftDeleteRequest } from '../../core/models/api/clients/client-soft-delete-request';
import { ClientApiService } from '../../core/services/api/client-api.service';
import { AdminModule } from '../admin.module';

@Injectable({
  providedIn: AdminModule
})
export class ClientAdminApiService extends ClientApiService {

  constructor(readonly http: HttpClient) {
    super(http);
  }

  public softDelete = (id: string, request: ClientSoftDeleteRequest): Observable<ClientApiModel> => {
    return this.http.patch<ClientApiModel>(`${this.baseUrl}/${id}`, request);
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
