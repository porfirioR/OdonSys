import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { combineLatest, map, of, switchMap, tap } from 'rxjs';
import { DoctorApiService } from '../../services/api/doctor-api.service';
import { InvoiceApiService } from '../../../workspace/services/invoice-api.service';
import { PaymentApiService } from '../../../workspace/services/payment-api.service';
import { InvoiceApiModel } from '../../../workspace/models/invoices/api/invoice-api-model';
import { DetailClientModel } from '../../models/view/detail-client-model';
import { InvoiceDetailModel } from '../../models/view/invoice-detail-model';
import { PaymentModel } from '../../../workspace/models/payments/payment-model';
import { FileModel } from '../../models/view/file-model';
import { InvoiceStatus } from '../../enums/invoice-status.enum';
import { Country } from '../../enums/country.enum';

import  * as fromTeethActions from '../../../core/store/teeth/tooth.actions';
import { selectTeeth } from '../../../core/store/teeth/tooth.selectors';
import { ToothModel } from '../../models/tooth/tooth-model';
import { selectClients } from '../../store/clients/client.selectors';
import  * as fromClientsActions from '../../../core/store/clients/client.actions';

@Component({
  selector: 'app-client-detail',
  templateUrl: './client-detail.component.html',
  styleUrls: ['./client-detail.component.scss']
})
export class ClientDetailComponent implements OnInit {
  protected load: boolean
  protected clientFormGroup = new FormGroup({
    name: new FormControl({ value: '', disabled: true }),
    document: new FormControl({ value: '', disabled: true }),
    ruc: new FormControl({ value: '0', disabled: true }),
    country: new FormControl({ value: Country.Paraguay, disabled: true }),
    phone: new FormControl({ value: '', disabled: true }),
    email: new FormControl({ value: '', disabled: true })
  })
  protected clientDetails = new Map<string, DetailClientModel>()
  protected invoiceStatus = InvoiceStatus
  protected invoicesSummary: InvoiceApiModel[] = []
  protected teeth: ToothModel[]

  constructor(
    private readonly activeRoute: ActivatedRoute,
    private readonly router: Router,
    private readonly doctorApiService: DoctorApiService,
    private readonly invoiceApiService: InvoiceApiService,
    private readonly paymentApiService: PaymentApiService,
    private domSanitizer: DomSanitizer,
    private store: Store,

  ) { }

  ngOnInit() {
    let loadingTooth = true
    const toothRowData$ = this.store.select(selectTeeth).pipe(tap(x => {
      if(loadingTooth && x.length === 0) {
        this.store.dispatch(fromTeethActions.componentLoadTeeth())
        loadingTooth = false
      }
    }))
    const clientId: string = this.activeRoute.snapshot.params['id']!
    let loadingClient = true
    const clientRowData$ = this.store.select(selectClients).pipe(tap(x => {
      if(loadingClient && x.length === 0) {
        this.store.dispatch(fromClientsActions.loadClients())
        loadingClient = false
      }
    }))
    const invoicesSummary$ = this.invoiceApiService.getInvoicesSummaryByClientId(clientId)
    combineLatest([clientRowData$, invoicesSummary$, toothRowData$])
    .subscribe({
      next: ([clients, invoicesSummary, teeth]) => {
        this.teeth = teeth
        const client = clients.find(x => x.id === clientId)!
        this.clientFormGroup.controls.name.setValue(`${client.name} ${client.middleName} ${client.surname} ${client.secondSurname}`)
        this.clientFormGroup.controls.email.setValue(client.email)
        this.clientFormGroup.controls.document.setValue(client.document)
        this.clientFormGroup.controls.ruc.setValue(client.ruc)
        this.clientFormGroup.controls.country.setValue(client.country)
        this.clientFormGroup.controls.phone.setValue(client.phone)
        this.invoicesSummary = invoicesSummary
        this.invoicesSummary.forEach(x => this.clientDetails.set(x.id, new DetailClientModel()))
        this.load = true
      }, error: (e) => {
        this.load = true
        throw e
      }
    })
  }

  protected getDetails = (invoiceId: string) => {
    const detailClientModel = this.clientDetails.get(invoiceId)!
    if (detailClientModel.hasData) {
      return
    }
    combineLatest([
    this.invoiceApiService.getInvoiceById(invoiceId),
    this.paymentApiService.getPaymentsByInvoiceId(invoiceId),
    this.invoiceApiService.previewInvoiceFile(invoiceId)
    ]).pipe(switchMap(([fullInvoice, paymentList, invoiceFiles]) => {
      const invoiceImageFiles = invoiceFiles.filter(x => x.format !== 'pdf')
      if (invoiceImageFiles.length > 0) {
        detailClientModel.hasFiles = true
        detailClientModel.invoiceImageFiles = invoiceImageFiles
          .sort((a, b) => new Date(a.dateCreated).getTime() - new Date(b.dateCreated).getTime())
          .map(x => new FileModel(x.url, this.domSanitizer.bypassSecurityTrustUrl(x.url), x.format, x.dateCreated, x.name, x.fullUrl))
      }
      const invoicePdfFiles = invoiceFiles.filter(x => x.format === 'pdf')
      if (invoicePdfFiles.length > 0) {
        detailClientModel.hasFiles = true
        detailClientModel.invoicePdfFiles = invoicePdfFiles
          .sort((a, b) => new Date(a.dateCreated).getTime() - new Date(b.dateCreated).getTime())
          .map(x => new FileModel(x.url, this.domSanitizer.bypassSecurityTrustUrl(x.url), x.format, x.dateCreated, x.name, x.fullUrl))
      }
      const invoiceDetails = fullInvoice.invoiceDetails
      detailClientModel.procedures = invoiceDetails.map(x => {
        const selectedTeed = this.teeth.filter(y => x.toothIds.includes(y.id))
        const teeth = selectedTeed.length === 0 ? 'Sin seleccionar' : selectedTeed.map(x => x.number).sort().join(', ')
        return new InvoiceDetailModel(x.id, invoiceId, x.procedure, x.procedurePrice, x.finalPrice, x.dateCreated, x.userCreated, teeth)
      })
      const userPayments = [... new Set(paymentList.map(x => x.userId))]
      const payments: PaymentModel[] = []
      if (userPayments.length === 0) {
        return of(payments)
      }
      const userPayments$ = userPayments.map(this.doctorApiService.getById)
      return combineLatest(userPayments$).pipe(map(users => {
        let remainingDebt = fullInvoice.total
        paymentList.forEach(payment => {
          const user = users.find(x => x!.id.compareString(payment.userId))
          remainingDebt -= payment.amount
          const paymentItem = new PaymentModel(user!.userName, payment.dateCreated, payment.amount, remainingDebt)
          payments.push(paymentItem)
        })
        return payments
      }))
    })).subscribe({
      next: (paymentList: PaymentModel[]) => {
        detailClientModel.payments = paymentList
        detailClientModel.hasPayments = paymentList.length > 0
        detailClientModel.hasData = true
      }, error: (e) => {
        
        throw e
      }
    })
  }

  protected downloadFile = (file: FileModel) => {
    fetch(file.fullUrl)
    .then(response => response.blob())
    .then(blob => {
      const downloadUrl = URL.createObjectURL(blob)
      const anchor: HTMLAnchorElement = document.createElement('a') as HTMLAnchorElement
      anchor.href = downloadUrl
      anchor.download = file.name
      anchor.click()
      URL.revokeObjectURL(downloadUrl)
    })
  }

  protected exit = () => {
    const currentUrl = this.router.url.split('/')
    currentUrl.pop()
    currentUrl.pop()
    this.router.navigate([currentUrl.join('/')])
  }
}
