import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { combineLatest, map, of, switchMap } from 'rxjs';
import { DoctorApiService } from '../../services/api/doctor-api.service';
import { ClientApiService } from '../../services/api/client-api.service';
import { InvoiceApiService } from '../../../workspace/services/invoice-api.service';
import { PaymentApiService } from '../../../workspace/services/payment-api.service';
import { InvoiceApiModel } from '../../../workspace/models/invoices/api/invoice-api-model';
import { DetailClientModel } from '../../models/view/detail-client-model';
import { InvoiceDetailModel } from '../../models/view/invoice-detail-model';
import { PaymentModel } from '../../../workspace/models/payments/payment-model';
import { InvoiceStatus } from '../../enums/invoice-status.enum';
import { Country } from '../../enums/country.enum';

@Component({
  selector: 'app-client-detail',
  templateUrl: './client-detail.component.html',
  styleUrls: ['./client-detail.component.scss']
})
export class ClientDetailComponent implements OnInit {
  protected load: boolean
  protected clientFormGroup = new FormGroup({
    name: new FormControl({ value: '', disabled: true }),
    document: new FormControl({ value: '', disabled: true}),
    ruc: new FormControl({ value: '0', disabled: true}),
    country: new FormControl({ value: Country.Paraguay, disabled: true}),
    phone: new FormControl({ value: '', disabled: true}),
    email: new FormControl({ value: '', disabled: true}),
  })
  protected formProcedure = new Map<string, DetailClientModel>()
  protected invoiceStatus = InvoiceStatus
  protected formGroup = new FormGroup({
    client: this.clientFormGroup
  })
  protected invoicesSummary: InvoiceApiModel[] = []

  constructor(
    private readonly activeRoute: ActivatedRoute,
    private readonly router: Router,
    private readonly doctorApiService: DoctorApiService,
    private readonly clientApiService: ClientApiService,
    private readonly invoiceApiService: InvoiceApiService,
    private readonly paymentApiService: PaymentApiService,
    ) { }

  ngOnInit() {
    const clientId: string = this.activeRoute.snapshot.params['id']!
    const client$ = this.clientApiService.getById(clientId)
    const invoicesSummary$ = this.invoiceApiService.getInvoicesSummaryByClientId(clientId)
    combineLatest([client$, invoicesSummary$])
    .subscribe({
      next: ([client, invoicesSummary]) => {
        this.clientFormGroup.controls.name.setValue(`${client.name} ${client.middleName} ${client.surname} ${client.secondSurname}`)
        this.clientFormGroup.controls.email.setValue(client.email)
        this.clientFormGroup.controls.document.setValue(client.document)
        this.clientFormGroup.controls.ruc.setValue(client.ruc)
        this.clientFormGroup.controls.country.setValue(client.country)
        this.clientFormGroup.controls.phone.setValue(client.phone)
        this.invoicesSummary = invoicesSummary
        this.invoicesSummary.forEach(x => this.formProcedure.set(x.id, new DetailClientModel()))
        this.load = true
      }, error: (e) => {
        this.load = true
        throw e
      }
    })
  }

  protected getDetails = (invoiceId: string) => {
    const detailClientModel = this.formProcedure.get(invoiceId)!
    if (detailClientModel.hasData) {
      return
    }
    combineLatest([
    this.invoiceApiService.getInvoiceById(invoiceId),
    this.paymentApiService.getPaymentsByInvoiceId(invoiceId),
    // this.invoiceApiService.previewInvoiceFile(invoiceId)
    ]).pipe(switchMap(([fullInvoice, paymentList]) => {
      const invoiceDetails = fullInvoice.invoiceDetails
      detailClientModel.procedures = invoiceDetails.map(x => new InvoiceDetailModel(x.id, invoiceId, x.procedure, x.procedurePrice, x.finalPrice, x.dateCreated, x.userCreated))
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

}
