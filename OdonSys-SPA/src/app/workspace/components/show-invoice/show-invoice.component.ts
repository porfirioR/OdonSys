import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { combineLatest, debounceTime, map, of, switchMap, tap } from 'rxjs';
import { ClientModel } from '../../../core/models/view/client-model';
import { InvoiceApiModel } from '../../models/invoices/api/invoice-api-model';
import { PaymentModel } from '../../models/payments/payment-model';
import { SelectModel } from '../../../core/models/view/select-model';
import { FileModel } from '../../../core/models/view/file-model';

import { selectClients } from '../../../core/store/clients/client.selectors';
import  * as fromClientsActions from '../../../core/store/clients/client.actions';

import  * as fromDoctorsActions from '../../../core/store/doctors/doctor.actions';
import { selectDoctor } from '../../../core/store/doctors/doctor.selectors';

import  * as fromTeethActions from '../../../core/store/teeth/tooth.actions';
import { selectTeeth } from '../../../core/store/teeth/tooth.selectors';

import { Country } from '../../../core/enums/country.enum';
import { UserFormGroup } from '../../../core/forms/user-form-group.form';
import { ProcedureFormGroup } from '../../../core/forms/procedure-form-group.form';
import { InvoiceApiService } from '../../services/invoice-api.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { PaymentApiService } from '../../services/payment-api.service';
import { ToothModel } from 'src/app/core/models/tooth/tooth-model';

@Component({
  selector: 'app-show-invoice',
  templateUrl: './show-invoice.component.html',
  styleUrls: ['./show-invoice.component.scss']
})
export class ShowInvoiceComponent implements OnInit {
  protected load: boolean = false
  protected saving: boolean = false
  protected invoice!: InvoiceApiModel
  protected proceduresValues: SelectModel[] = []
  protected clientsValues: Map<string, string> = new Map<string, string>()
  protected clientFormGroup = new FormGroup<UserFormGroup>({
    name: new FormControl({ value: '', disabled: true }),
    middleName: new FormControl({ value: '', disabled: true}),
    surname: new FormControl({ value: '', disabled: true}),
    secondSurname: new FormControl({ value: '', disabled: true}),
    document: new FormControl({ value: '', disabled: true}),
    ruc: new FormControl({ value: 0, disabled: true}),
    country: new FormControl({ value: Country.Paraguay, disabled: true}),
    phone: new FormControl({ value: '', disabled: true}),
    email: new FormControl({ value: '', disabled: true}),
    active: new FormControl(true)
  })
  protected payments: PaymentModel[] = []
  protected hasPayments = false
  protected invoicePdfFiles: FileModel[] = []
  protected invoiceImageFiles: FileModel[] = []
  private teeth: ToothModel[]

  public formGroup = new FormGroup({
    client: this.clientFormGroup,
    procedures: new FormArray<FormGroup<ProcedureFormGroup>>([]),
    subTotal: new FormControl({ value: 0, disabled: true}),
    total: new FormControl({ value: 0, disabled: true})
  })

  constructor(
    private store: Store,
    private readonly invoiceApiService: InvoiceApiService,
    private readonly alertService: AlertService,
    private readonly router: Router,
    private readonly activeRoute: ActivatedRoute,
    private readonly paymentApiService: PaymentApiService,
    private domSanitizer: DomSanitizer
  ) { }

  ngOnInit() {
    const invoiceId: string = this.activeRoute.snapshot.params['id']
    let loadingClient = true
    const clientRowData$ = this.store.select(selectClients).pipe(tap(x => {
      if(loadingClient && x.length === 0) {
        this.store.dispatch(fromClientsActions.loadClients())
        loadingClient = false
      }
    }))
    let loadingTooth = true
    const toothRowData$ = this.store.select(selectTeeth).pipe(tap(x => {
      if(loadingTooth && x.length === 0) {
        this.store.dispatch(fromTeethActions.componentLoadTeeth())
        loadingTooth = false
      }
    }))
    combineLatest([
      this.invoiceApiService.getInvoiceById(invoiceId),
      clientRowData$,
      this.paymentApiService.getPaymentsByInvoiceId(invoiceId),
      this.invoiceApiService.previewInvoiceFile(invoiceId),
      toothRowData$
    ]).pipe(switchMap(([invoice, clients, paymentList, invoiceFiles, teeth]) => {
      this.teeth = teeth
      const invoiceImageFiles = invoiceFiles.filter(x => x.format !== 'pdf')
      if (invoiceImageFiles.length > 0) {
        this.invoiceImageFiles = invoiceImageFiles
          .sort((a, b) => new Date(a.dateCreated).getTime() - new Date(b.dateCreated).getTime())
          .map(x => new FileModel(x.url, this.domSanitizer.bypassSecurityTrustUrl(x.url), x.format, x.dateCreated, x.name, x.fullUrl))
      }
      const invoicePdfFiles = invoiceFiles.filter(x => x.format === 'pdf')
      if (invoicePdfFiles.length > 0) {
        this.invoicePdfFiles = invoicePdfFiles
          .sort((a, b) => new Date(a.dateCreated).getTime() - new Date(b.dateCreated).getTime())
          .map(x => new FileModel(x.url, this.domSanitizer.bypassSecurityTrustUrl(x.url), x.format, x.dateCreated, x.name, x.fullUrl))
      }
      this.invoice = invoice
      const client = clients.find(x => x.id.compareString(this.invoice.clientId))!
      this.setClient(client)
      this.setInvoiceProcedures(invoice)
      const userPayments = [... new Set(paymentList.map(x => x.userId))]
      const payments: PaymentModel[] = []
      if (userPayments.length === 0) {
        return of(payments)
      }
      const userPayments$ = userPayments.map(userId => {
        this.store.dispatch(fromDoctorsActions.loadDoctor({ doctorId: userId }))
        return this.store.select(selectDoctor(userId)).pipe(debounceTime(500))
      })
      return combineLatest(userPayments$).pipe(map(users => {
        let remainingDebt = this.invoice.total
        paymentList.forEach(payment => {
          const user = users.find(x => x!.id.compareString(payment.userId))
          remainingDebt -= payment.amount
          const paymentItem = new PaymentModel(user!.userName, payment.dateCreated, payment.amount, remainingDebt)
          payments.push(paymentItem)
        })
        return payments
      }))
    }))
    .subscribe({
      next: (paymentList: PaymentModel[]) => {
        this.payments = paymentList
        this.hasPayments = paymentList.length > 0
        this.load = true
      }, error: (e) => {
        this.alertService.showError('Error al obtener datos. Vuelve a intentarlo y si el error persiste contacta con el administrador.')
        this.exit()
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

  private setInvoiceProcedures = (invoice: InvoiceApiModel) => {
    const formArray = this.formGroup.controls.procedures as FormArray
    invoice.invoiceDetails.forEach(invoiceDetail => {
      const selectedTeed = this.teeth.filter(y => invoiceDetail.toothIds.includes(y.id))
      const teeth = selectedTeed.length === 0 ? 'Sin seleccionar' : selectedTeed.map(x => x.number).sort().join(', ')
      const procedureFormGroup = new FormGroup<ProcedureFormGroup>({
        id: new FormControl(invoiceDetail.id),
        name: new FormControl(invoiceDetail.procedure),
        price: new FormControl(invoiceDetail.procedurePrice),
        finalPrice: new FormControl(invoiceDetail.finalPrice),
        xRays: new FormControl(false),
        teethSelected:  new FormControl(teeth)
      })
      procedureFormGroup.disable()
      formArray.push(procedureFormGroup)
    })
    this.calculatePrices()
  }

  private calculatePrices = (): void => {
    let subTotal = 0
    let total = 0
    this.formGroup.controls.procedures.controls.forEach(x => {
      subTotal += x.value.price!
      total += x.value.finalPrice!
    })
    this.formGroup.patchValue({
      subTotal: subTotal,
      total: total
    })
  }

  private setClient = (client: ClientModel) => {
    this.formGroup.controls.client.patchValue({
      name: client!.name,
      middleName: client!.middleName,
      surname: client!.surname,
      secondSurname: client!.secondSurname,
      document: client!.document,
      ruc: client!.ruc,
      phone: client!.phone,
      country: client!.country,
      email: client!.email,
      active: client!.active
    })
    this.formGroup.controls.client.disable()
  }

}
