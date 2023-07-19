import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { combineLatest } from 'rxjs';
import { AlertService } from '../../services/shared/alert.service';
import { DoctorApiService } from '../../services/api/doctor-api.service';
import { ClientApiService } from '../../services/api/client-api.service';
import { InvoiceApiService } from '../../../workspace/services/invoice-api.service';
import { PaymentApiService } from '../../../workspace/services/payment-api.service';
import { InvoiceApiModel } from '../../../workspace/models/invoices/api/invoice-api-model';
import { DetailClientModel } from '../../models/view/detail-client-model';
import { Country } from '../../enums/country.enum';
import { ProcedureModel } from '../../models/procedure/procedure-model';
import { InvoiceDetailModel } from '../../models/view/invoice-detail-model';

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

  protected formGroup = new FormGroup({
    client: this.clientFormGroup
  })


  protected invoicesSummary: InvoiceApiModel[] = []

  constructor(
    private readonly alertService: AlertService,
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
    // this.paymentApiService.getPaymentsByInvoiceId(invoiceId),
    // this.invoiceApiService.previewInvoiceFile(invoiceId)
    ]).subscribe({
      next: ([fullInvoice, 
        // payments, files
      ]) => {
        const invoiceDetails = fullInvoice.invoiceDetails
        detailClientModel.procedures = invoiceDetails.map(x => new InvoiceDetailModel(x.id, invoiceId, x.procedure, x.procedurePrice, x.finalPrice, x.dateCreated, x.userCreated))
        detailClientModel.hasData = true
        // this.formProcedure.set(invoiceId, detailClientModel)
      }, error: (e) => {
        throw e
      }
    })
  }

}
