import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable, tap } from 'rxjs';
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

@Component({
  selector: 'app-upsert-invoice',
  templateUrl: './upsert-invoice.component.html',
  styleUrls: ['./upsert-invoice.component.scss']
})
export class UpsertInvoiceComponent implements OnInit {
  protected clientRowData$!: Observable<ClientModel[]>
  protected procedureRowData$!: Observable<ProcedureModel[]>
  protected countries: Map<string, string> = new Map<string, string>()

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
    this.procedureRowData$ = this.store.select(selectProcedures).pipe(tap(x => {
      if(loadingProcedure && x.length === 0) {
        this.store.dispatch(fromProceduresActions.loadProcedures())
        loadingProcedure = false
      }
    }))
  }

}
