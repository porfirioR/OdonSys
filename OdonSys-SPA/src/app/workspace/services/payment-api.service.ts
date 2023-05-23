import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PaymentApiModel } from '../models/payments/payment-api-model';
import { PaymentRequest } from '../models/payments/payment-request';

@Injectable({
  providedIn: 'root'
})
export class PaymentApiService {
  private baseUrl = `${environment.apiUrl}/payment`

  constructor(
    private readonly http: HttpClient
  ) { }

  public getPaymentsByInvoiceId = (id: string): Observable<PaymentApiModel[]> => {
    return this.http.get<PaymentApiModel[]>(`${this.baseUrl}/${id}`)
  }

  public registerPayment = (request: PaymentRequest): Observable<PaymentApiModel> => {
    return this.http.post<PaymentApiModel>(`${this.baseUrl}`, request)
  }
}
