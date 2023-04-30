import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable, of } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { ClientApiModel } from '../../../../core/models/api/clients/client-api-model';
import { CreateClientRequest } from '../../../../core/models/api/clients/create-client-request';
import { UpdateClientRequest } from '../../../../core/models/api/clients/update-client-request';
import { ClientApiService } from '../../../../core/services/api/client-api.service';
import { AlertService } from '../../../../core/services/shared/alert.service';
import { UserInfoService } from '../../../../core/services/shared/user-info.service';
import { CustomValidators } from '../../../../core/helpers/custom-validators';
import { MethodHandler } from '../../../../core/helpers/method-handler';
import { EnumHandler } from '../../../../core/helpers/enum-handler';
import { UserFormGroup } from '../../../../core/forms/user-form-group.form';
import { Country } from '../../../../core/enums/country.enum';
import { Permission } from '../../../../core/enums/permission.enum';

@Component({
  selector: 'app-upsert-client',
  templateUrl: './upsert-client.component.html',
  styleUrls: ['./upsert-client.component.scss']
})
export class UpsertClientComponent implements OnInit {
  protected title: string = 'Registrar '
  protected load: boolean = false
  protected saving: boolean = false
  protected formGroup!: FormGroup<UserFormGroup>
  protected countries: Map<string, string> = new Map<string, string>()
  private id = ''
  private fullFieldEdit = false

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly clientApiService: ClientApiService,
    private readonly alertService: AlertService,
    private readonly location: Location,
    private userInfoService: UserInfoService,
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
    this.saving = true
    const request$ = this.clientRequest()
    request$.subscribe({
      next: () => {
        const message = `Paciente ${this.id ? 'actualizado' : 'registrado'} actualizado con éxito.`
        this.alertService.showSuccess(message)
        this.close()
      },
      error: (error) => {
        this.saving = false
        throw error
      }
    })
  }

  private loadValues = () => {
    this.id = this.activatedRoute.snapshot.params['id']
    const client$ = this.id ? this.clientApiService.getById(this.id) : of<ClientApiModel>({ } as ClientApiModel)
    client$.subscribe({
      next: (client: ClientApiModel) => {
        this.formGroup = new FormGroup<UserFormGroup>({
          name: new FormControl(this.id ? client.name : '', [Validators.required, Validators.maxLength(25)]),
          middleName: new FormControl(this.id ? client.secondSurname : '', [Validators.maxLength(25)]),
          surname: new FormControl(this.id ? client.surname : '', [Validators.required, Validators.maxLength(25)]),
          secondSurname: new FormControl(this.id ? client.secondSurname : '', [Validators.maxLength(25)]),
          document: new FormControl(this.id ? client.document : '', [Validators.required, Validators.maxLength(15), Validators.min(0)]),
          ruc: new FormControl({ value: this.id && client.ruc ? client.ruc : 0, disabled: true }, [Validators.required, Validators.maxLength(1), Validators.min(0), Validators.max(9)]),
          country: new FormControl(this.id ? Country[client.country]! as unknown as Country : Country.Paraguay, [Validators.required]),
          phone: new FormControl(this.id ? client.phone : '', [Validators.required, Validators.maxLength(15), CustomValidators.checkPhoneValue()]),
          email: new FormControl(this.id ? client.email : '', [Validators.required, Validators.maxLength(20), Validators.email]),
          active: new FormControl(this.id ? client.active : true)
        })
        this.formGroup.controls.document.valueChanges.pipe(
          debounceTime(500),
        ).subscribe({
          next: (document: string | null) => {
            const checkDigit = MethodHandler.calculateCheckDigit(document!, +this.formGroup.controls.country.value!)
            this.formGroup.controls.ruc.setValue(checkDigit)
          }
        })
        this.formGroup.controls.country.valueChanges.subscribe(() => this.formGroup.controls.document.updateValueAndValidity())
        if (this.id) {
          this.title = 'Actualizar '
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

  private clientRequest = (): Observable<ClientApiModel> => {
    if (this.id) {
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
      return this.clientApiService.updateClient(updateClient)
    } else {
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
      return this.clientApiService.createClient(newClient)
    }
  }

}
