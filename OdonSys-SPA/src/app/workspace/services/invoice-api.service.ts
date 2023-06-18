import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CreateInvoiceRequest } from '../models/invoices/api/create-invoice-request';
import { InvoicePatchRequest } from '../models/invoices/api/invoice-patch-request';
import { InvoiceApiModel } from '../models/invoices/api/invoice-api-model';
import { UploadFileRequest } from '../../core/models/api/files/upload-file-request';
import { FileApiModel } from '../../core/models/api/files/file-api-model';

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

  public getInvoiceById = (id: string): Observable<InvoiceApiModel> => {
    return this.http.get<InvoiceApiModel>(`${this.baseUrl}/${id}`)
  }

  public getMyInvoices = (): Observable<InvoiceApiModel[]> => {
    return this.http.get<InvoiceApiModel[]>(`${this.baseUrl}/my-invoices`)
  }

  public createInvoice = (request: CreateInvoiceRequest): Observable<InvoiceApiModel> => {
    return this.http.post<InvoiceApiModel>(`${this.baseUrl}`, request)
  }

  public changeStatus = (id: string, request: InvoicePatchRequest): Observable<InvoiceApiModel> => {
    return this.http.patch<InvoiceApiModel>(`${this.baseUrl}/${id}`, [request]);
  }

  public uploadInvoiceFiles = (request: UploadFileRequest): Observable<string[]> => {
    const formData = new FormData()
    formData.append('id', request.referenceId)
    request.files.forEach((file) => formData.append(`files`, file))
    return this.http.post<string[]>(`${this.baseUrl}/upload-invoice-files`, formData)
  }

  public previewInvoiceFile = (id: string): Observable<FileApiModel[]> => {
    return this.http.get<FileApiModel[]>(`${this.baseUrl}/preview-invoice-files/${id}`)
  }

  public fullInvoiceFile = (id: string): Observable<FileApiModel[]> => {
    return this.http.get<FileApiModel[]>(`${this.baseUrl}/full-invoice-files/${id}`)
  }

}
