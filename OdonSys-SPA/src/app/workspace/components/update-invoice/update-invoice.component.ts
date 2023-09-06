import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { Store } from '@ngrx/store';
import { ActivatedRoute, Router } from '@angular/router';
import { combineLatest, tap } from 'rxjs';
import { Country } from '../../../core/enums/country.enum';
import { InvoiceApiService } from '../../services/invoice-api.service';
import { selectClients } from '../../../core/store/clients/client.selectors';
import  * as fromClientsActions from '../../../core/store/clients/client.actions';
import { InvoiceFormGroup } from '../../../core/forms/invoice-form-group.form';
import { InvoiceDetailFormGroup } from '../../../core/forms/invoice-detail-form-group.form';

@Component({
  selector: 'app-update-invoice',
  templateUrl: './update-invoice.component.html',
  styleUrls: ['./update-invoice.component.scss']
})
export class UpdateInvoiceComponent implements OnInit {
  protected load: boolean

  protected clientFormGroup = new FormGroup({
    name: new FormControl({ value: '', disabled: true }),
    document: new FormControl({ value: '', disabled: true }),
    ruc: new FormControl({ value: '0', disabled: true }),
    country: new FormControl({ value: Country.Paraguay, disabled: true }),
    phone: new FormControl({ value: '', disabled: true }),
    email: new FormControl({ value: '', disabled: true })
  })

  protected invoiceFormGroup = new FormGroup<InvoiceFormGroup>({
    id: new FormControl(),
    iva10: new FormControl(),
    totalIva: new FormControl(),
    subTotal: new FormControl(),
    total: new FormControl(),
    invoiceDetails: new FormArray<FormGroup<InvoiceDetailFormGroup>>([])
  })

  constructor(
    private readonly activeRoute: ActivatedRoute,
    private readonly invoiceApiService: InvoiceApiService,
    private store: Store,
    private readonly router: Router,
  ) { }

  ngOnInit() {
    const invoiceId: string = this.activeRoute.snapshot.params['id']
    let loadingClient = true
    const clientRowData$ = this.store.select(selectClients).pipe(tap(x => {
      if(loadingClient && x.length === 0) {
        this.store.dispatch(fromClientsActions.loadClients())
        loadingClient = false
      }
    }))
    const invoice$ = this.invoiceApiService.getInvoiceById(invoiceId)
    combineLatest([invoice$, clientRowData$]).subscribe({
      next: ([invoice, clients]) => {
        const client = clients.find(x => x.id === invoice.clientId)!
        this.clientFormGroup.controls.name.setValue(`${client.name} ${client.middleName} ${client.surname} ${client.secondSurname}`)
        this.clientFormGroup.controls.email.setValue(client.email)
        this.clientFormGroup.controls.document.setValue(client.document)
        this.clientFormGroup.controls.ruc.setValue(client.ruc)
        this.clientFormGroup.controls.country.setValue(client.country)
        this.clientFormGroup.controls.phone.setValue(client.phone)

        this.invoiceFormGroup.controls.id.setValue(invoice.id)
        this.invoiceFormGroup.controls.iva10.setValue(invoice.iva10)
        this.invoiceFormGroup.controls.totalIva.setValue(invoice.totalIva)
        this.invoiceFormGroup.controls.subTotal.setValue(invoice.subTotal)
        this.invoiceFormGroup.controls.total.setValue(invoice.total)
        const invoiceDetails = new FormArray(invoice.invoiceDetails.map(x => new FormGroup<InvoiceDetailFormGroup>({
          id: new FormControl(x.id),
          procedure: new FormControl(x.procedure),
          procedurePrice: new FormControl(x.procedurePrice),
          finalPrice: new FormControl(x.finalPrice)
        })))
        this.invoiceFormGroup.controls.invoiceDetails = invoiceDetails
        this.load = true
      }, error: (e) => {
        this.load = true
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
