import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Store, select } from '@ngrx/store';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { debounceTime, map } from 'rxjs/operators';
import { CreateClientRequest } from '../../models/api/clients/create-client-request';
import { UpdateClientRequest } from '../../models/api/clients/update-client-request';
import { SelectModel } from '../../models/view/select-model';
import { selectClients } from '../../store/clients/client.selectors';
import * as fromClientsActions from '../../store/clients/client.actions';
import { savingSelector } from '../../store/saving/saving.selector';
import { UserInfoService } from '../../services/shared/user-info.service';
import { CustomValidators } from '../../helpers/custom-validators';
import { MethodHandler } from '../../helpers/method-handler';
import { EnumHandler } from '../../helpers/enum-handler';
import { UserFormGroup } from '../../forms/user-form-group.form';
import { Country } from '../../enums/country.enum';
import { Permission } from '../../enums/permission.enum';

@Component({
  selector: 'app-upsert-client',
  templateUrl: './upsert-client.component.html',
  styleUrls: ['./upsert-client.component.scss']
})
export class UpsertClientComponent implements OnInit {
  public formGroup = new FormGroup<UserFormGroup>({
    name: new FormControl('', [Validators.required, Validators.maxLength(25)]),
    middleName: new FormControl('', [Validators.maxLength(25)]),
    surname: new FormControl('', [Validators.required, Validators.maxLength(25)]),
    secondSurname: new FormControl('', [Validators.maxLength(25)]),
    document: new FormControl('', [Validators.required, Validators.maxLength(15), Validators.min(0), Validators.minLength(6)]),
    ruc: new FormControl(0, [Validators.required, Validators.maxLength(1), Validators.min(0), Validators.max(9)]),
    country: new FormControl(Country.Paraguay, [Validators.required]),
    phone: new FormControl('', [Validators.required, Validators.maxLength(15), CustomValidators.checkPhoneValue()]),
    email: new FormControl('', [Validators.required, Validators.maxLength(20), Validators.email]),
    active: new FormControl(true)
  })
  public ignorePreventUnsavedChanges: boolean = false
  protected title: string = 'Registrar '
  protected saving$: Observable<boolean> = this.store.select(savingSelector)
  protected saving: boolean = false
  protected countries: SelectModel[] = []
  private id = ''
  private fullFieldEdit = false

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly location: Location,
    private readonly router: Router,
    private userInfoService: UserInfoService,
    private store: Store,
  ) {
    this.countries = EnumHandler.getCountries()
  }

  ngOnInit() {
    this.fullFieldEdit = this.userInfoService.havePermission(Permission.FullFieldUpdateClients)
    this.loadValues()
  }

  protected close = () => {
    this.location.back()
  }

  protected save = (): void => {
    if (this.formGroup.invalid) { return }
    this.ignorePreventUnsavedChanges = true
    this.id ? this.update() : this.create()
  }

  private loadValues = () => {
    this.id = this.activatedRoute.snapshot.params['id']
    const isUpdateUrl = this.activatedRoute.snapshot.url[1].path === 'actualizar'
    const client$ = this.store.pipe(
      select(selectClients),
      map(x => this.id ? x.find(y => y.id === this.id) ?? undefined : undefined)
    )
    client$.subscribe({
      next: (data) => {
        //todo workspace my patients
        if (isUpdateUrl && !data) {
          this.router.navigate(['admin/pacientes/registrar'])
        }
        this.formGroupValueChanges()
        if (this.id && data) {
          this.title = 'Actualizar '
          this.formGroup.controls.name.setValue(data.name)
          this.formGroup.controls.middleName.setValue(data.middleName)
          this.formGroup.controls.surname.setValue(data.surname)
          this.formGroup.controls.secondSurname.setValue(data.secondSurname)
          this.formGroup.controls.document.setValue(data.document)
          this.formGroup.controls.ruc.setValue(data.ruc)
          this.formGroup.controls.country.setValue(Country[data.country]! as unknown as Country)
          this.formGroup.controls.phone.setValue(data.phone)
          this.formGroup.controls.email.setValue(data.email)
          this.formGroup.controls.active.setValue(data.active)
          this.formGroup.controls.ruc.disable()
          if (!this.fullFieldEdit) {
            this.formGroup.controls.document.disable()
            this.formGroup.controls.country.disable()
            this.formGroup.controls.email.disable()
          }
        }
      }
    })
  }

  private formGroupValueChanges = () => {
    this.formGroup.controls.document.valueChanges.pipe(
      debounceTime(500),
    ).subscribe({
      next: (document: string | null) => {
        const checkDigit = MethodHandler.calculateCheckDigit(document!, this.formGroup.controls.country.value!)
        this.formGroup.controls.ruc.setValue(checkDigit)
      }
    })
    this.formGroup.controls.country.valueChanges.subscribe(() => this.formGroup.controls.document.updateValueAndValidity())
  }

  private create = () => {
    const newClient = new CreateClientRequest(
      this.formGroup.value.name!,
      this.formGroup.value.middleName!,
      this.formGroup.value.surname!,
      this.formGroup.value.secondSurname!,
      this.formGroup.value.phone!,
      this.formGroup.value.country!,
      this.formGroup.value.email!,
      this.formGroup.value.document!,
      this.formGroup.controls.ruc.value!.toString(),
    )
    this.store.dispatch(fromClientsActions.addClient({ client: newClient }))
  }

  private update = () => {
    const updateClient = new UpdateClientRequest(
      this.id,
      this.formGroup.value.name!,
      this.formGroup.value.middleName!,
      this.formGroup.value.surname!,
      this.formGroup.value.secondSurname!,
      this.formGroup.value.phone!,
      this.formGroup.controls.country.value!,
      this.formGroup.controls.email.value!,
      this.formGroup.controls.document.value!,
      this.formGroup.value.active!
    )
    this.store.dispatch(fromClientsActions.updateClient({ client: updateClient }))
  }
}
