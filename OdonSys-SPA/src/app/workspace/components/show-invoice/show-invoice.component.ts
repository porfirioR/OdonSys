import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { Store } from '@ngrx/store';
import { combineLatest, tap } from 'rxjs';

import { ClientModel } from '../../../core/models/view/client-model';
import { ProcedureModel } from '../../../core/models/procedure/procedure-model';
import { InvoiceApiModel } from '../../models/invoices/api/invoice-api-model';

import { selectClients } from '../../../core/store/clients/client.selectors';
import  * as fromClientsActions from '../../../core/store/clients/client.actions';

import { Country } from '../../../core/enums/country.enum';


import { UserFormGroup } from '../../../core/forms/user-form-group.form';
import { ProcedureFormGroup } from '../../../core/forms/procedure-form-group.form';

import { InvoiceApiService } from '../../services/invoice-api.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { SelectModel } from '../../../core/models/view/select-model';

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

    combineLatest([this.invoiceApiService.getInvoiceById(invoiceId), clientRowData$ ])
    .subscribe({
      next: ([invoice, clients]) => {
        this.invoice = invoice
        const client = clients.find(x => x.id === this.invoice.clientId)!
        this.setClient(client)
        this.setInvoiceProcedures(invoice)
        this.load = true
      }, error: (e) => {
        this.alertService.showError('Error al traer los recuros. si el error persiste contacte con el administrador')
        this.exit()
        throw e
      }
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
    invoice.invoiceDetails.forEach(procedure => {
      const procedureFormGroup = new FormGroup<ProcedureFormGroup>({
        id: new FormControl(procedure.id),
        name: new FormControl(procedure.procedure),
        price: new FormControl(procedure.procedurePrice),
        finalPrice: new FormControl(procedure.finalPrice)
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
    this.formGroup.controls.subTotal.setValue(subTotal)
    this.formGroup.controls.total.setValue(total)
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
