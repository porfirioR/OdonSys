import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClientApiService {
  protected baseUrl = `${environment.apiUrl}/clients`;

  constructor(protected readonly http: HttpClient) { }

}
