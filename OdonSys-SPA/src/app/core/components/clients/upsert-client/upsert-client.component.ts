import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Store, select } from '@ngrx/store';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { debounceTime, map } from 'rxjs/operators';
import { CreateClientRequest } from '../../../../core/models/api/clients/create-client-request';
import { UpdateClientRequest } from '../../../../core/models/api/clients/update-client-request';
import { selectClients } from '../../../../core/store/clients/client.selectors';
import * as fromClientsActions from '../../../../core/store/clients/client.actions';
import { UserInfoService } from '../../../../core/services/shared/user-info.service';
import { CustomValidators } from '../../../../core/helpers/custom-validators';
import { MethodHandler } from '../../../../core/helpers/method-handler';
import { EnumHandler } from '../../../../core/helpers/enum-handler';
import { UserFormGroup } from '../../../../core/forms/user-form-group.form';
import { Country } from '../../../../core/enums/country.enum';
import { Permission } from '../../../../core/enums/permission.enum';
import { savingSelector } from '../../../../core/store/saving/saving.selector';

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
    document: new FormControl('', [Validators.required, Validators.maxLength(15), Validators.min(0)]),
    ruc: new FormControl(0, [Validators.required, Validators.maxLength(1), Validators.min(0), Validators.max(9)]),
    country: new FormControl(Country.Paraguay, [Validators.required]),
    phone: new FormControl('', [Validators.required, Validators.maxLength(15), CustomValidators.checkPhoneValue()]),
    email: new FormControl('', [Validators.required, Validators.maxLength(20), Validators.email]),
    active: new FormControl(true)
  })
  protected title: string = 'Registrar '
  protected load: boolean = false
  protected saving$: Observable<boolean> = this.store.select(savingSelector)
  protected saving: boolean = false
  protected countries: Map<string, string> = new Map<string, string>()
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
        this.load = true
      }, error: (e) => {
        this.load = true
        throw e
      }
    })
  }

  private formGroupValueChanges = () => {
    this.formGroup.controls.document.valueChanges.pipe(
      debounceTime(500),
    ).subscribe({
      next: (document: string | null) => {
        const checkDigit = MethodHandler.calculateCheckDigit(document!, +this.formGroup.controls.country.value!)
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
      this.formGroup.value.document!,
      this.formGroup.controls.ruc.value!.toString(),
      this.formGroup.value.country!,
      this.formGroup.value.phone!,
      this.formGroup.value.email!
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
