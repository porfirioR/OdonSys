import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

import { selectActiveClients } from '../../../core/store/clients/client.selectors';
import  * as fromClientsActions from '../../../core/store/clients/client.actions';
import  * as fromProceduresActions from '../../../core/store/procedures/procedure.actions';
import { selectActiveProcedures } from '../../../core/store/procedures/procedure.selectors';

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
  selector: 'app-upsert-invoice',
  templateUrl: './upsert-invoice.component.html',
  styleUrls: ['./upsert-invoice.component.scss']
})
export class UpsertInvoiceComponent implements OnInit {
  protected load: boolean = false
  protected saving: boolean = false
  protected clients!: ClientModel[]
  private procedures!: ProcedureModel[]
  protected countries: SelectModel[] = []
  protected proceduresValues: SelectModel[] = []
  protected clientsValues: Map<string, string> = new Map<string, string>()
  protected clientFormGroup = new FormGroup<UserFormGroup>({
    name: new FormControl('', [Validators.required, Validators.maxLength(25)]),
    middleName: new FormControl('', [Validators.maxLength(25)]),
    surname: new FormControl('', [Validators.required, Validators.maxLength(25)]),
    secondSurname: new FormControl('', [Validators.maxLength(25)]),
    document: new FormControl('', [Validators.required, Validators.maxLength(15), Validators.min(0), Validators.minLength(6)]),
    ruc: new FormControl({ value: 0, disabled: true}, [Validators.required, Validators.maxLength(1), Validators.min(0), Validators.max(9)]),
    country: new FormControl(Country.Paraguay, [Validators.required]),
    phone: new FormControl('', [Validators.required, Validators.maxLength(15), CustomValidators.checkPhoneValue()]),
    email: new FormControl('', [Validators.required, Validators.maxLength(20), Validators.email]),
    active: new FormControl(true)
  })

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
    private readonly router: Router
  ) {
    this.countries = EnumHandler.getCountries()
  }

  ngOnInit() {
    let loadingClient = true
    const clientRowData$ = this.store.select(selectActiveClients).pipe(tap(x => {
      if(loadingClient && x.length === 0) {
        this.store.dispatch(fromClientsActions.loadClients())
        loadingClient = false
      }
    }))
    let loadingProcedure = true
    const procedureRowData$ = this.store.select(selectActiveProcedures).pipe(tap(x => {
      if(loadingProcedure && x.length === 0) {
        this.store.dispatch(fromProceduresActions.loadProcedures())
        loadingProcedure = false
      }
    }))
    combineLatest([clientRowData$, procedureRowData$]).subscribe({
      next: ([clients, procedures]) => {
        this.clients = clients
        clients.forEach(x => this.clientsValues.set(x.id, x.name))
        this.procedures = procedures
        procedures.forEach(x => this.proceduresValues.push(new SelectModel(x.id, x.name)))
        this.load = true
      }, error: (e) => {
        this.alertService.showError('Error al traer los recuros. si el error persiste contacte con el administrador')
        this.exit()
        throw e
      }
    })
    this.formGroupValueChanges()
    this.formGroup.controls.procedures.addValidators(this.minimumOneSelectedValidator)
  }

  protected cleanClient = () => {
    this.formGroup.controls.client.reset()
    this.formGroup.controls.client.patchValue({ country: Country.Paraguay })
    this.formGroup.controls.clientId.setValue('', { emitEvent: false })
    this.formGroup.controls.client.enable()
    this.formGroup.controls.client.controls.ruc.disable()
  }

  protected removeProcedure = (id: string) => {
    const formArray = this.formGroup.controls.procedures as FormArray
    const index = formArray.controls.findIndex((x) => (x as FormGroup).controls.id.value === id)
    formArray.removeAt(index)
    this.proceduresValues.find(x => x.key === id)!.disabled = false
    this.calculatePrices()
  }

  protected save = () => {
    if (this.formGroup.invalid) { return }
    this.saving = true
    this.generateRequest().subscribe({
      next: () => {
        this.saving = false
        this.alertService.showSuccess('Factura creada con éxito')
        this.formGroup.reset()
        this.exit()
      }, error: (e) => {
        this.saving = false
        throw e
      }
    })
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
    this.formGroup.controls.clientId.valueChanges.pipe(
      debounceTime(500),
      filter(x => !!x)
    ).subscribe({
      next: (clientId) => {
        const client = this.clients.find(x => x.id === clientId)
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

  private minimumOneSelectedValidator = (abstractControl: AbstractControl): ValidationErrors | null => {
    const procedures = abstractControl as FormArray<FormGroup<ProcedureFormGroup>>
    return procedures.controls.some(x => x) ? null : { noneSelected : true }
  }

  private generateRequest = (): Observable<InvoiceApiModel> => {
    const clientId = this.formGroup.value.clientId
    if (clientId) {
      const selectedClient = this.clients.find(x => x.id === clientId)!
      const userId = this.userInfoService.getUserData().id
      if (selectedClient.doctors.find(x => x.id === userId)) {
        return this.createInvoice(clientId)
      }
      return this.doctorApiService.assignClientToUser(new AssignClientRequest(userId, clientId)).pipe(switchMap(x => this.createInvoice(clientId)))
    }
    // Create Client
    return this.clientApiService.createClient(
      new CreateClientRequest(
        this.formGroup.controls.client.controls.name.value!,
        this.formGroup.controls.client.controls.middleName.value!,
        this.formGroup.controls.client.controls.surname.value!,
        this.formGroup.controls.client.controls.secondSurname.value!,
        this.formGroup.controls.client.controls.phone.value!,
        this.formGroup.controls.client.controls.country.value!,
        this.formGroup.controls.client.controls.email.value!,
        this.formGroup.controls.client.controls.document.value!,
        this.formGroup.controls.client.controls.ruc.value!.toString()
      )
    ).pipe(switchMap(client => this.createInvoice(client.id)))
  }

  private createInvoice = (clientId: string) => {
    // Create Client Procedure
    const procedures = this.formGroup.controls.procedures.controls
    const clientProcedures$ = procedures.map(x => this.clientProcedureApiService.createClientProcedure(new CreateClientProcedureRequest(x.controls.id.value!, clientId)))
    return forkJoin(clientProcedures$).pipe(switchMap(clientProcedures => {
      // Create Invoice
      const invoiceDetails: CreateInvoiceDetailRequest[] = clientProcedures.map(x => {
        const selectedProcedure = procedures.find(y => y.controls.id.value === x.procedureId)!
        return new CreateInvoiceDetailRequest(x.id, selectedProcedure.controls.price.value!, selectedProcedure.controls.finalPrice.value!)
      })
      const invoice = new CreateInvoiceRequest('invoiceNumber', 1, 1, this.formGroup.controls.subTotal.value!, this.formGroup.controls.total.value!, 'timbrado', InvoiceStatus.Nuevo, clientId, invoiceDetails)
      return this.invoiceApiService.createInvoice(invoice)
    }))
  }
}
