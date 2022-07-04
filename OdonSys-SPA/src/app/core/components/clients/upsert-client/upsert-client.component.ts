import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable, of } from 'rxjs';
import { filter, switchMap } from 'rxjs/operators';
import { ClientApiModel } from '../../../../core/models/api/clients/client-api-model';
import { CreateClientRequest } from '../../../../core/models/api/clients/create-client-request';
import { UpdateClientRequest } from '../../../../core/models/api/clients/update-client-request';
import { CustomValidators } from '../../../../core/helpers/custom-validators';
import { EnumToMap } from '../../../../core/helpers/enumToMap';
import { ClientApiService } from '../../../../core/services/api/client-api.service';
import { AlertService } from '../../../../core/services/shared/alert.service';

@Component({
  selector: 'app-upsert-client',
  templateUrl: './upsert-client.component.html',
  styleUrls: ['./upsert-client.component.scss']
})
export class UpsertClientComponent implements OnInit {
  public title: string = 'Registrar ';
  public load: boolean = false;
  public saving: boolean = false;
  public formGroup: FormGroup = new FormGroup({});
  public countries: Map<string, string> = new Map<string, string>();
  private id = '';
  private fullEdit = false;
  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly location: Location,
    private readonly clientApiService: ClientApiService,
    private readonly alertService: AlertService,

  ) {
    this.countries = EnumToMap.getCountries();
  }

  ngOnInit() {
    this.loadValues();
  }

  protected close = () => {
    this.location.back();
  }

  protected save = (): void => {
    if (this.formGroup.invalid) { return; }
    this.saving = true;
    const request$ = this.clientRequest();
    request$.subscribe({
      next: () => {
        this.alertService.showSuccess('Datos guardados.');
        this.location.back();
      },
      error: (error) => {
        this.saving = false;
        throw error;
      }
    });
  }

  private loadValues = () => {
    this.activatedRoute.params.pipe(
      switchMap(params => {
        this.id = params.id;
        const client$ = this.id ? this.clientApiService.getById(this.id) : of<ClientApiModel>({ } as ClientApiModel);
        return client$;
      })
    ).subscribe({
      next: (client: ClientApiModel ) => {
        this.formGroup = new FormGroup({
          name: new FormControl(this.id ? client.name : '', [Validators.required, Validators.maxLength(25)]),
          middleName: new FormControl(this.id ? client.middleLastName : '', [Validators.maxLength(25)]),
          lastName: new FormControl(this.id ? client.lastName : '', [Validators.required, Validators.maxLength(25)]),
          middleLastName: new FormControl(this.id ? client.middleLastName : '', [Validators.maxLength(25)]),
          document: new FormControl(this.id ? client.document: '', [Validators.required, Validators.maxLength(15), Validators.min(0)]),
          ruc: new FormControl({ value: this.id && client.ruc ? client.ruc : 0, disabled: true }, [Validators.required, Validators.maxLength(1), Validators.min(0), Validators.max(9)]),
          phone: new FormControl(this.id ? client.phone:'', [Validators.required, Validators.maxLength(15), CustomValidators.checkPhoneValue()]),
          country: new FormControl(this.id ? client.country:'', [Validators.required]),
          email: new FormControl(this.id ? client.email:'', [Validators.required, Validators.maxLength(20), Validators.email]),
        });
        this.formGroup.controls.document.valueChanges.pipe(filter(val => val && val.length >= 6)).subscribe({
          next: (x: string) => {
            if(isNaN(+x)) {
              let multiplier = 2;
              const module = 11;
              const reverseDocument = x.split('').reverse();
              
            }
          }
        });
        if (this.id) {
          this.title = 'Actualizar ';
          if (!this.fullEdit) {
            this.formGroup.controls.document.disable();
            this.formGroup.controls.country.disable();
            this.formGroup.controls.email.disable();
          }
        }
        this.load = true;
      }, error: (e) => {
        this.load = true;
        throw e;
      }
    });
  }

  private clientRequest = (): Observable<ClientApiModel> => {
    if (this.id) {
      const updateClient = new UpdateClientRequest(
        this.id,
        this.formGroup.controls.name.value,
        this.formGroup.controls.middleName.value,
        this.formGroup.controls.lastName.value,
        this.formGroup.controls.middleLastName.value,
        this.formGroup.controls.phone.value,
        this.formGroup.controls.country.value,
        this.formGroup.controls.email.value
      );
      return this.clientApiService.updateClient(updateClient);
    } else {
      const newClient = new CreateClientRequest(
        this.formGroup.controls.name.value,
        this.formGroup.controls.middleName.value,
        this.formGroup.controls.lastName.value,
        this.formGroup.controls.middleLastName.value,
        this.formGroup.controls.document.value,
        this.formGroup.controls.ruc.value,
        this.formGroup.controls.country.value,
        this.formGroup.controls.phone.value,
        this.formGroup.controls.email.value
      );
      return this.clientApiService.createClient(newClient);
    }
  }

}
