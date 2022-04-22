import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { ToothApiModel } from '../../models/tooth/tooth-api-model';

@Injectable({
  providedIn: 'root'
})
export class ToothApiService {
  private baseUrl = `${environment.apiUrl}/procedure`;

  constructor(private http: HttpClient) { }

  public getAll = (): Observable<ToothApiModel[]> => {
    return this.http.get<ToothApiModel[]>(`${this.baseUrl}`);
  }

}
