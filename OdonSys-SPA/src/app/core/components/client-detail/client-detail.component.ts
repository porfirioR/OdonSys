import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AlertService } from '../../services/shared/alert.service';
import { DoctorApiService } from '../../services/api/doctor-api.service';
import { ClientApiService } from '../../services/api/client-api.service';
import { Country } from '../../enums/country.enum';

@Component({
  selector: 'app-client-detail',
  templateUrl: './client-detail.component.html',
  styleUrls: ['./client-detail.component.scss']
})
export class ClientDetailComponent implements OnInit {
  protected load: boolean
  protected clientFormGroup = new FormGroup({
    name: new FormControl({ value: '', disabled: true }),
    document: new FormControl({ value: '', disabled: true}),
    ruc: new FormControl({ value: '0', disabled: true}),
    country: new FormControl({ value: Country.Paraguay, disabled: true}),
    phone: new FormControl({ value: '', disabled: true}),
    email: new FormControl({ value: '', disabled: true}),
  })

  protected formGroup = new FormGroup({
    client: this.clientFormGroup,
  })

  constructor(
    private store: Store,
    private readonly alertService: AlertService,
    private domSanitizer: DomSanitizer,
    private readonly activeRoute: ActivatedRoute,
    private readonly router: Router,
    private readonly doctorApiService: DoctorApiService,
    private readonly clientApiService: ClientApiService
  ) { }

  ngOnInit() {
    const id: string = this.activeRoute.snapshot.params['id']!
    this.clientApiService.getById(id).subscribe({
      next: (client) => {
        this.clientFormGroup.controls.name.setValue(`${client.name} ${client.middleName} ${client.surname} ${client.secondSurname}`)
        this.clientFormGroup.controls.email.setValue(client.email)
        this.clientFormGroup.controls.document.setValue(client.document)
        this.clientFormGroup.controls.ruc.setValue(client.ruc)
        this.clientFormGroup.controls.country.setValue(client.country)
        this.clientFormGroup.controls.phone.setValue(client.phone)
        this.load = true
      }, error: (e) => {
        this.load = true
        throw e
      }
    })
  }

}
