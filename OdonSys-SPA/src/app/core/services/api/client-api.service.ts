import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { ClientApiModel } from '../../models/api/clients/client-api-model';
import { CreateClientRequest } from '../../models/api/clients/create-client-request';
import { UpdateClientRequest } from '../../models/api/clients/update-client-request';

@Injectable({
  providedIn: 'root'
})
export class ClientApiService {
  protected baseUrl = `${environment.apiUrl}/clients`;

  constructor(protected readonly http: HttpClient) { }

  public getById = (id: string): Observable<ClientApiModel> => {
    return this.http.get<ClientApiModel>(`${this.baseUrl}/${id}`);
  }

  public getByDocumentId = (documentId: string): Observable<ClientApiModel> => {
    return this.http.get<ClientApiModel>(`${this.baseUrl}/document/${documentId}`);
  }

  public createClient = (request: CreateClientRequest): Observable<ClientApiModel> => {
    return this.http.post<ClientApiModel>(`${this.baseUrl}`, request);
  }

  public updateClient = (request: UpdateClientRequest): Observable<ClientApiModel> => {
    return this.http.put<ClientApiModel>(`${this.baseUrl}`, request);
  }

  public getDoctorPatients = (): Observable<ClientApiModel[]> => {
    return this.http.get<ClientApiModel[]>(`${this.baseUrl}/patients`);
  }
}
