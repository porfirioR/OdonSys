import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable, debounceTime, tap } from 'rxjs';
import { selectClients } from '../../../core/store/clients/client.selectors';
import { ClientModel } from '../../../core/models/view/client-model';
import { ProcedureModel } from '../../../core/models/procedure/procedure-model';
import  * as fromClientsActions from '../../../core/store/clients/client.actions';
import  * as fromProceduresActions from '../../../core/store/procedures/procedure.actions';
import { selectProcedures } from '../../../core/store/procedures/procedure.selectors';
import { UserFormGroup } from '../../../core/forms/user-form-group.form';
import { Country } from '../../../core/enums/country.enum';
import { CustomValidators } from '../../../core/helpers/custom-validators';
import { EnumHandler } from '../../../core/helpers/enum-handler';
import { ProcedureFormGroup } from 'src/app/core/forms/procedure-form-group.form';

@Component({
  selector: 'app-upsert-invoice',
  templateUrl: './upsert-invoice.component.html',
  styleUrls: ['./upsert-invoice.component.scss']
})
export class UpsertInvoiceComponent implements OnInit {
  protected clientRowData$!: Observable<ClientModel[]>
  protected procedures!: ProcedureModel[]
  protected countries: Map<string, string> = new Map<string, string>()
  protected proceduresValues: Map<string, string> = new Map<string, string>()
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
    total: new FormControl({ value: 0, disabled: true})
  })


  constructor(
    private store: Store,
  ) {
    this.countries = EnumHandler.getCountries()
  }

  ngOnInit() {
    let loadingClient = true
    this.clientRowData$ = this.store.select(selectClients).pipe(tap(x => {
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
  }

  protected removeProcedure = (id: string) => {
    const formArray = this.formGroup.controls.procedures as FormArray
    const index = formArray.controls.findIndex((x) => (x as FormGroup).controls.id.value === id)
    formArray.removeAt(index)
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
}
