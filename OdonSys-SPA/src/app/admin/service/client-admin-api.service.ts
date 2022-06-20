import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BasicServiceModule } from '../../basic-service.module';
import { ClientApiModel } from '../../core/models/api/clients/client-api-model';
import { ClientPatchRequest } from '../../core/models/api/clients/client-patch-request';
import { ClientApiService } from '../../core/services/api/client-api.service';

@Injectable({
  providedIn: BasicServiceModule
})
export class ClientAdminApiService extends ClientApiService {

  constructor(readonly http: HttpClient) {
    super(http);
  }

  public clientVisibility = (id: string, request: ClientPatchRequest): Observable<ClientApiModel> => {
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
