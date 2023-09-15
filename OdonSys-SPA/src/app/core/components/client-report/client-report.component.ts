import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientApiService } from '../../services/api/client-api.service';
import { FormControl, FormGroup } from '@angular/forms';
import { Country } from '../../enums/country.enum';

@Component({
  selector: 'app-client-report',
  templateUrl: './client-report.component.html',
  styleUrls: ['./client-report.component.scss']
})
export class ClientReportComponent implements OnInit {
  protected loading = true
  private id: string

  protected clientFormGroup = new FormGroup({
    name: new FormControl({ value: '', disabled: true }),
    document: new FormControl({ value: '', disabled: true }),
    ruc: new FormControl({ value: '0', disabled: true }),
    country: new FormControl({ value: Country.Paraguay, disabled: true }),
    phone: new FormControl({ value: '', disabled: true }),
    email: new FormControl({ value: '', disabled: true })
  })

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly clientApiService: ClientApiService,
    private readonly router: Router,
  ) { }

  ngOnInit() {
    this.id = this.activatedRoute.snapshot.params['id']
    this.clientApiService.getClientReport(this.id).subscribe({
      next: (value) => {
        this.clientFormGroup.controls.name.setValue(`${value.clientModel.name} ${value.clientModel.middleName} ${value.clientModel.surname} ${value.clientModel.secondSurname}`)
        this.clientFormGroup.controls.email.setValue(value.clientModel.email)
        this.clientFormGroup.controls.document.setValue(value.clientModel.document)
        this.clientFormGroup.controls.ruc.setValue(value.clientModel.ruc)
        this.clientFormGroup.controls.country.setValue(value.clientModel.country)
        this.clientFormGroup.controls.phone.setValue(value.clientModel.phone)
        
        this.loading = false
      }, error: (e) => {
        this.loading = false
        throw e
      }
    })
  }

  protected exit = () => {
    const currentUrl = this.router.url.split('/')
    currentUrl.pop()
    currentUrl.pop()
    this.router.navigate([currentUrl.join('/')])
  }

}
