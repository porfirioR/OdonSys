import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CreateInvoiceRequest } from '../models/invoices/api/create-invoice-request';
import { InvoicePatchRequest } from '../models/invoices/api/invoice-patch-request';
import { InvoiceApiModel } from '../models/invoices/api/invoice-api-model';

@Injectable({
  providedIn: 'root'
})
export class InvoiceApiService {
  private baseUrl = `${environment.apiUrl}/invoice`

  constructor(
    private readonly http: HttpClient
  ) { }

  public getInvoices = (): Observable<InvoiceApiModel[]> => {
    return this.http.get<InvoiceApiModel[]>(`${this.baseUrl}`)
  }

  public createInvoice = (request: CreateInvoiceRequest): Observable<InvoiceApiModel> => {
    return this.http.post<InvoiceApiModel>(`${this.baseUrl}`, request)
  }

  public changeStatus = (id: string, request: InvoicePatchRequest): Observable<InvoiceApiModel> => {
    return this.http.patch<InvoiceApiModel>(`${this.baseUrl}/${id}`, [request]);
  }

}
