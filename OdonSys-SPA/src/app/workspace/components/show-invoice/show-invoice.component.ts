import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AbstractControl, FormArray, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable, combineLatest, debounceTime, filter, forkJoin, switchMap, tap } from 'rxjs';

import { ClientModel } from '../../../core/models/view/client-model';
import { ProcedureModel } from '../../../core/models/procedure/procedure-model';
import { CreateClientRequest } from '../../../core/models/api/clients/create-client-request';
import { CreateClientProcedureRequest } from '../../models/client-procedures/create-client-procedure-request';
import { CreateInvoiceRequest } from '../../models/invoices/api/create-invoice-request';
import { CreateInvoiceDetailRequest } from '../../models/invoices/api/create-invoice-detail-request';
import { InvoiceApiModel } from '../../models/invoices/api/invoice-api-model';
import { AssignClientRequest } from '../../../core/models/api/clients/assign-client-request';

import { selectActiveClients, selectClients } from '../../../core/store/clients/client.selectors';
import  * as fromClientsActions from '../../../core/store/clients/client.actions';
import  * as fromProceduresActions from '../../../core/store/procedures/procedure.actions';
import { selectActiveProcedures, selectProcedures } from '../../../core/store/procedures/procedure.selectors';

import { Country } from '../../../core/enums/country.enum';
import { InvoiceStatus } from '../../../core/enums/invoice-status.enum';

import { CustomValidators } from '../../../core/helpers/custom-validators';
import { EnumHandler } from '../../../core/helpers/enum-handler';
import { MethodHandler } from '../../../core/helpers/method-handler';

import { UserFormGroup } from '../../../core/forms/user-form-group.form';
import { ProcedureFormGroup } from '../../../core/forms/procedure-form-group.form';

import { ClientApiService } from '../../../core/services/api/client-api.service';
import { InvoiceApiService } from '../../services/invoice-api.service';
import { ClientProcedureApiService } from '../../services/client-procedure-api.service';
import { DoctorApiService } from '../../../core/services/api/doctor-api.service';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
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
  private procedures!: ProcedureModel[]

  public formGroup = new FormGroup({
    client: this.clientFormGroup,
    procedure: new FormControl(''),
    procedures: new FormArray<FormGroup<ProcedureFormGroup>>([]),
    subTotal: new FormControl({ value: 0, disabled: true}),
    total: new FormControl({ value: 0, disabled: true}),
    clientId: new FormControl('')
  })
  constructor(
    private store: Store,
    private readonly clientApiService: ClientApiService,
    private readonly invoiceApiService: InvoiceApiService,
    private readonly clientProcedureApiService: ClientProcedureApiService,
    private readonly doctorApiService: DoctorApiService,
    private userInfoService: UserInfoService,
    private readonly alertService: AlertService,
    private readonly router: Router,
    private readonly activeRoute: ActivatedRoute,
  ) { }

  ngOnInit() {
    const invoiceId: string = this.activeRoute.snapshot.data['id']
    let loadingClient = true
    const clientRowData$ = this.store.select(selectClients).pipe(tap(x => {
      if(loadingClient && x.length === 0) {
        this.store.dispatch(fromClientsActions.loadClients())
        loadingClient = false
      }
    }))
    let loadingProcedure = true
    const procedureRowData$ = this.store.select(selectProcedures).pipe(tap(x => {
      if(loadingProcedure && x.length === 0) {
        this.store.dispatch(fromProceduresActions.loadProcedures())
        loadingProcedure = false
      }
    }))

    this.invoiceApiService.getInvoiceById(invoiceId).pipe(switchMap(invoice => {
      this.invoice = invoice
      return combineLatest([clientRowData$, procedureRowData$])
    }))
    .subscribe({
      next: ([clients, procedures]) => {
        this.setClient(clients)

        this.procedures = procedures
        this.load = true
      }, error: (e) => {
        this.alertService.showError('Error al traer los recuros. si el error persiste contacte con el administrador')
        this.exit()
        throw e
      }
    })
    this.formGroupValueChanges()
  }

  protected exit = () => {
    const currentUrl = this.router.url.split('/')
    currentUrl.pop()
    this.router.navigate([currentUrl.join('/')])
  }

  private formGroupValueChanges = () => {
    this.formGroup.controls.procedure.valueChanges.pipe(
      debounceTime(500),
      filter(x => !!x)
    ).subscribe({
      next: (procedure) => {
        const currentProcedure = this.procedures.find(x => x.id === procedure)!
        const formArray = this.formGroup.controls.procedures as FormArray
        if (!formArray.controls.find((x) => (x as FormGroup).controls.id.value === currentProcedure.id)) {
          const procedureFormGroup = new FormGroup<ProcedureFormGroup>({
            id: new FormControl(currentProcedure.id),
            name: new FormControl(currentProcedure.name),
            price: new FormControl(currentProcedure.price),
            finalPrice: new FormControl(currentProcedure.price, Validators.min(0))
          })
          formArray.push(procedureFormGroup)
          this.proceduresValues.find(x => x.key === currentProcedure.id)!.disabled = true
          procedureFormGroup.valueChanges.subscribe({ next: () => this.calculatePrices() })
        }
        this.calculatePrices()
        this.formGroup.controls.procedure.setValue('', { onlySelf: true, emitEvent: false })
      }
    })
    this.formGroup.controls.client.controls.document.valueChanges.pipe(
      debounceTime(500),
    ).subscribe({
      next: (document: string | null) => {
        const checkDigit = MethodHandler.calculateCheckDigit(document!, this.formGroup.controls.client.controls.country.value!)
        this.formGroup.controls.client.controls.ruc.setValue(checkDigit)
      }
    })
    this.formGroup.controls.client.controls.country.valueChanges.subscribe(() => this.formGroup.controls.client.controls.document.updateValueAndValidity())
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

  private setClient = (clients: ClientModel[]) => {
    const client = clients.find(x => x.id === this.invoice.clientId)
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
