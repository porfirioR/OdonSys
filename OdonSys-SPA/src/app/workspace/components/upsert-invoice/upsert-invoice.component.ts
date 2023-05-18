import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { debounceTime, tap } from 'rxjs';
import { ClientModel } from '../../../core/models/view/client-model';
import { ProcedureModel } from '../../../core/models/procedure/procedure-model';
import { selectClients } from '../../../core/store/clients/client.selectors';
import  * as fromClientsActions from '../../../core/store/clients/client.actions';
import  * as fromProceduresActions from '../../../core/store/procedures/procedure.actions';
import { selectProcedures } from '../../../core/store/procedures/procedure.selectors';
import { Country } from '../../../core/enums/country.enum';
import { CustomValidators } from '../../../core/helpers/custom-validators';
import { EnumHandler } from '../../../core/helpers/enum-handler';
import { UserFormGroup } from '../../../core/forms/user-form-group.form';
import { ProcedureFormGroup } from '../../../core/forms/procedure-form-group.form';
import { ClientApiService } from 'src/app/core/services/api/client-api.service';
import { InvoiceApiService } from '../../services/invoice-api.service';
import { ClientProcedureApiService as ClientProcedureApiService } from '../../services/client-procedure-api.service';
import { CreateClientRequest } from 'src/app/core/models/api/clients/create-client-request';
import { UpdateClientRequest } from 'src/app/core/models/api/clients/update-client-request';

@Component({
  selector: 'app-upsert-invoice',
  templateUrl: './upsert-invoice.component.html',
  styleUrls: ['./upsert-invoice.component.scss']
})
export class UpsertInvoiceComponent implements OnInit {
  protected clients!: ClientModel[]
  private procedures!: ProcedureModel[]
  protected countries: Map<string, string> = new Map<string, string>()
  protected proceduresValues: Map<string, string> = new Map<string, string>()
  protected clientsValues: Map<string, string> = new Map<string, string>()
  public clientFormGroup = new FormGroup<UserFormGroup>({
    name: new FormControl('', [Validators.required, Validators.maxLength(25)]),
    middleName: new FormControl('', [Validators.maxLength(25)]),
    surname: new FormControl('', [Validators.required, Validators.maxLength(25)]),
    secondSurname: new FormControl('', [Validators.maxLength(25)]),
    document: new FormControl('', [Validators.required, Validators.maxLength(15), Validators.min(0)]),
    ruc: new FormControl(0, [Validators.required, Validators.maxLength(1), Validators.min(0), Validators.max(9)]),
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
    private readonly clientProcedureApiService: ClientProcedureApiService
  ) {
    this.countries = EnumHandler.getCountries()
  }

  ngOnInit() {
    let loadingClient = true
    const clientRowData$ = this.store.select(selectClients).pipe(tap(x => {
      if(loadingClient && x.length === 0) {
        this.store.dispatch(fromClientsActions.loadClients())
        loadingClient = false
      }
    }))
    clientRowData$.subscribe({
      next: (clients) => {
        this.clients = clients
        clients.forEach(x => this.clientsValues.set(x.id, x.name))
      }
    })
    let loadingProcedure = true
    const procedureRowData$ = this.store.select(selectProcedures).pipe(tap(x => {
      if(loadingProcedure && x.length === 0) {
        this.store.dispatch(fromProceduresActions.loadProcedures())
        loadingProcedure = false
      }
    }))
    procedureRowData$.subscribe({
      next: (procedures) => {
        this.procedures = procedures
        procedures.forEach(x => this.proceduresValues.set(x.id, x.name))
      }
    })
    this.formGroup.controls.procedure.valueChanges.pipe(
      debounceTime(500)
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
          procedureFormGroup.valueChanges.subscribe({
            next: (value) => {
              this.calculatePrices()
            }
          })
        }
        this.calculatePrices()
        this.formGroup.controls.procedure.setValue('', { onlySelf: true, emitEvent: false })
      }
    })
    this.formGroup.controls.clientId.valueChanges.pipe(
      debounceTime(500)
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
    this.formGroup.controls.procedures.addValidators(this.minimumOneSelectedValidator)
  }

  protected cleanClient = () => {
    this.formGroup.controls.client.patchValue({
      name: '',
      middleName: '',
      surname: '',
      secondSurname: '',
      document: '',
      ruc: '',
      phone: '',
      country: Country.Paraguay,
      email: '',
      active: true
    })
    this.formGroup.controls.clientId.setValue('', { emitEvent: false })
    this.formGroup.controls.client.enable()
  }

  protected removeProcedure = (id: string) => {
    const formArray = this.formGroup.controls.procedures as FormArray
    const index = formArray.controls.findIndex((x) => (x as FormGroup).controls.id.value === id)
    formArray.removeAt(index)
    this.calculatePrices()
  }

  private calculatePrices = () => {
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

  protected save = () => {
    if (this.formGroup.invalid) { return }
    const clientRequest$ = this.getClientRequest()
  }

  private getClientRequest = () => {
    const clientRequest$ = this.formGroup.value.clientId ?
      this.clientApiService.updateClient(
        new UpdateClientRequest(
          this.formGroup.value.clientId!,
          this.formGroup.controls.client.controls.name.value!,
          this.formGroup.controls.client.controls.middleName.value!,
          this.formGroup.controls.client.controls.surname.value!,
          this.formGroup.controls.client.controls.secondSurname.value!,
          this.formGroup.controls.client.controls.phone.value!,
          this.formGroup.controls.client.controls.country.value!,
          this.formGroup.controls.client.controls.email.value!,
          this.formGroup.controls.client.controls.document.value!,
          this.formGroup.controls.client.controls.active.value!
        )
      ) :
      this.clientApiService.createClient(
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
      )
    return clientRequest$
  }
}
