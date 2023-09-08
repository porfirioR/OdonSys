import { Component, HostListener, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { combineLatest, debounceTime, take, tap } from 'rxjs';
import { Country } from '../../../core/enums/country.enum';
import { InvoiceApiService } from '../../services/invoice-api.service';
import { AlertService } from '../../../core/services/shared/alert.service';

import { selectClients } from '../../../core/store/clients/client.selectors';
import  * as fromClientsActions from '../../../core/store/clients/client.actions';
import { selectTeeth } from '../../../core/store/teeth/tooth.selectors';
import  * as fromTeethActions from '../../../core/store/teeth/tooth.actions';

import { InvoiceFormGroup } from '../../../core/forms/invoice-form-group.form';
import { InvoiceDetailFormGroup } from '../../../core/forms/invoice-detail-form-group.form';
import { ToothModalComponent } from '../../../core/components/tooth-modal/tooth-modal.component';
import { ToothModalModel } from '../../../core/models/view/tooth-modal-model';
import { UpdateInvoiceDetailRequest } from '../../models/invoices/api/update-invoice-detail-request';
import { UpdateInvoiceRequest } from '../../models/invoices/api/update-invoice-request';

@Component({
  selector: 'app-update-invoice',
  templateUrl: './update-invoice.component.html',
  styleUrls: ['./update-invoice.component.scss']
})
export class UpdateInvoiceComponent implements OnInit {
  protected load: boolean = false
  public saving: boolean = false

  protected clientFormGroup = new FormGroup({
    name: new FormControl({ value: '', disabled: true }),
    document: new FormControl({ value: '', disabled: true }),
    ruc: new FormControl({ value: '0', disabled: true }),
    country: new FormControl({ value: Country.Paraguay, disabled: true }),
    phone: new FormControl({ value: '', disabled: true }),
    email: new FormControl({ value: '', disabled: true })
  })

  protected invoiceFormGroup = new FormGroup<InvoiceFormGroup>({
    id: new FormControl('', [Validators.required]),
    iva10: new FormControl(0, [Validators.required]),
    totalIva: new FormControl(0, [Validators.required]),
    subTotal: new FormControl(0, [Validators.required]),
    total: new FormControl(0, [Validators.required, Validators.min(1)]),
    invoiceDetails: new FormArray<FormGroup<InvoiceDetailFormGroup>>([]),
    paymentAmount: new FormControl(0)
  })

  constructor(
    private readonly activeRoute: ActivatedRoute,
    private readonly invoiceApiService: InvoiceApiService,
    private readonly router: Router,
    private readonly alertService: AlertService,
    private store: Store,
    private modalService: NgbModal,
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
    let loadingTeeth = true
    const teethRowData$ = this.store.select(selectTeeth).pipe(tap(x => {
      if(loadingTeeth && x.length === 0) {
        this.store.dispatch(fromTeethActions.componentLoadTeeth())
        loadingTeeth = false
      }
    }))
    const invoice$ = this.invoiceApiService.getInvoiceById(invoiceId).pipe(debounceTime(1000), take(1))
    combineLatest([clientRowData$, teethRowData$, invoice$]).subscribe({
      next: ([clients, teeth, invoice]) => {
        const client = clients.find(x => x.id === invoice.clientId)!
        this.clientFormGroup.controls.name.setValue(`${client.name} ${client.middleName} ${client.surname} ${client.secondSurname}`)
        this.clientFormGroup.controls.email.setValue(client.email)
        this.clientFormGroup.controls.document.setValue(client.document)
        this.clientFormGroup.controls.ruc.setValue(client.ruc)
        this.clientFormGroup.controls.country.setValue(client.country)
        this.clientFormGroup.controls.phone.setValue(client.phone)

        this.invoiceFormGroup.controls.id.setValue(invoice.id)
        this.invoiceFormGroup.controls.iva10.setValue(invoice.iva10)
        this.invoiceFormGroup.controls.totalIva.setValue(invoice.totalIva)
        this.invoiceFormGroup.controls.subTotal.setValue(invoice.subTotal)
        this.invoiceFormGroup.controls.total.setValue(invoice.total)
        this.invoiceFormGroup.controls.total.addValidators(Validators.min(invoice.paymentAmount))

        this.invoiceFormGroup.controls.paymentAmount.setValue(invoice.paymentAmount)
        const invoiceDetails = new FormArray(invoice.invoiceDetails.map(x => {
          const formGroup = new FormGroup<InvoiceDetailFormGroup>({
            id: new FormControl(x.id, [Validators.required]),
            procedure: new FormControl(x.procedure, [Validators.required]),
            procedurePrice: new FormControl(x.procedurePrice, [Validators.required]),
            finalPrice: new FormControl(x.finalPrice, [Validators.required]),
            toothIds: new FormArray<FormControl>(x.toothIds.map(x => new FormControl(x))),
            teethSelected: new FormControl<string>(x.toothIds.map(x => teeth.find(y => y.id === x)!.number).sort().join(', '))
          })
          formGroup.controls.finalPrice.valueChanges.pipe(debounceTime(100)).subscribe({ next: () => this.calculatePrices() })
          return formGroup
        }))
        this.invoiceFormGroup.controls.invoiceDetails = invoiceDetails
        this.calculatePrices()
        this.load = true
      }, error: (e) => {
        this.load = true
        throw e
      }
    })
  }

  protected exit = (): void => {
    const currentUrl = this.router.url.split('/')
    currentUrl.pop()
    currentUrl.pop()
    this.router.navigate([currentUrl.join('/')])
  }

  protected selectTeeth = (i: number): void => {
    const invoiceDetail: FormGroup<InvoiceDetailFormGroup> = this.invoiceFormGroup.controls.invoiceDetails!.controls[i]
    const modalRef = this.modalService.open(ToothModalComponent, {
      size: 'lg',
      backdrop: 'static',
      keyboard: false
    })
    modalRef.componentInstance.toothIds = invoiceDetail.value.toothIds
    modalRef.result.then((teethIds: ToothModalModel[]) => {
      if (!!teethIds) {
        invoiceDetail.controls.toothIds?.clear()
        invoiceDetail.controls.teethSelected?.setValue(teethIds.map(x => x.number).sort().join(', '))
        teethIds.forEach(x => invoiceDetail.controls.toothIds?.push(new FormControl(x.id)))
      }
    }, () => {})
  }

  protected save = (): void => {
    if (this.invoiceFormGroup.invalid) { return }
    this.saving = true
    const invoiceFormGroupValue = this.invoiceFormGroup.value
    const invoiceDetailsRequest = this.invoiceFormGroup.controls.invoiceDetails?.controls.map(x => {
      const toothIds: string[] = x.controls.toothIds?.value.filter((y:string | null) => !!y).map(x => x as string)!
      const invoiceDetails = new UpdateInvoiceDetailRequest(x.value.id!, x.value.finalPrice!, toothIds)
      return invoiceDetails
    })!
    const updateRequest = new UpdateInvoiceRequest(invoiceFormGroupValue.id!, invoiceFormGroupValue.iva10!, invoiceFormGroupValue.totalIva!, invoiceFormGroupValue.subTotal!, invoiceFormGroupValue.total!, invoiceDetailsRequest)
    this.invoiceApiService.updateInvoice(updateRequest).subscribe({
      next: () => {
        this.saving = false
        this.alertService.showSuccess('Factura actualizada con éxito')
        this.invoiceFormGroup.reset()
        this.clientFormGroup.reset()
        this.exit()
      }, error: (e) => {
        this.saving = false
        throw e
      }
    })
  }

  private calculatePrices = (): void => {
    let subTotal = 0
    let total = 0
    this.invoiceFormGroup.controls.invoiceDetails?.controls.forEach(x => {
      subTotal += x.value.procedurePrice!
      total += x.value.finalPrice!
    })
    this.invoiceFormGroup.controls.subTotal.setValue(subTotal)
    this.invoiceFormGroup.controls.total.setValue(total)
    this.invoiceFormGroup.controls.total.updateValueAndValidity()
  }
}
