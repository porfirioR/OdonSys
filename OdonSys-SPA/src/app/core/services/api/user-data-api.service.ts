import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { UserDataApiModel } from '../../models/api/user-data-api-model';

@Injectable({
  providedIn: 'root'
})
export class UserDataApiService {
  private baseUrl = `${environment.apiUrl}/userdata`;

  constructor(private http: HttpClient) { }

  public getUserData = (): Observable<UserDataApiModel> => {
    return this.http.get<UserDataApiModel>(this.baseUrl);
  }
}
