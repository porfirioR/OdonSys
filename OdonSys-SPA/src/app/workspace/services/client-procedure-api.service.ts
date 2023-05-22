import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { CreateClientProcedureRequest } from '../models/client-procedures/create-client-procedure-request';
import { ClientProcedureApiModel } from '../models/client-procedures/client-procedure-api-model';
import { Observable } from 'rxjs';
import { UpdateClientProcedureRequest } from '../models/client-procedures/update-client-procedure-request';

@Injectable({
  providedIn: 'root'
})
export class ClientProcedureApiService {
  private baseUrl = `${environment.apiUrl}/clientProcedure`

  constructor(
    private readonly http: HttpClient,
  ) { }

  public createClientProcedure = (request: CreateClientProcedureRequest): Observable<ClientProcedureApiModel> => {
    return this.http.post<ClientProcedureApiModel>(`${this.baseUrl}`, request)
  }

  public updateClientProcedure = (request: UpdateClientProcedureRequest): Observable<ClientProcedureApiModel> => {
    return this.http.put<ClientProcedureApiModel>(`${this.baseUrl}`, request)
  }

}
