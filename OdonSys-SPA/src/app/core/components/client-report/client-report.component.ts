import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { Country } from '../../enums/country.enum';
import { ClientApiService } from '../../services/api/client-api.service';
import { InvoiceDetailReportApiModel } from '../../models/api/clients/invoice-detail-report-api-model';
import { Store } from '@ngrx/store';
import  * as fromTeethActions from '../../../core/store/teeth/tooth.actions';
import { selectTeeth } from '../../../core/store/teeth/tooth.selectors';
import { combineLatest, debounceTime, tap } from 'rxjs';

@Component({
  selector: 'app-client-report',
  templateUrl: './client-report.component.html',
  styleUrls: ['./client-report.component.scss']
})
export class ClientReportComponent implements OnInit {
  protected clientFormGroup = new FormGroup({
    name: new FormControl({ value: '', disabled: true }),
    document: new FormControl({ value: '', disabled: true }),
    ruc: new FormControl({ value: '0', disabled: true }),
    country: new FormControl({ value: Country.Paraguay, disabled: true }),
    phone: new FormControl({ value: '', disabled: true }),
    email: new FormControl({ value: '', disabled: true })
  })
  protected loading = true
  private id: string
  protected invoiceDetailReportApiModelList: InvoiceDetailReportApiModel[] = []
  protected total: number

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly clientApiService: ClientApiService,
    private readonly router: Router,
    private store: Store
  ) { }

  ngOnInit() {
    this.id = this.activatedRoute.snapshot.params['id']
    let loadingTooth = true
    const toothRowData$ = this.store.select(selectTeeth).pipe(tap(x => {
      if(loadingTooth && x.length === 0) {
        this.store.dispatch(fromTeethActions.componentLoadTeeth())
        loadingTooth = false
      }
    }))
    combineLatest([
      this.clientApiService.getClientReport(this.id),
      toothRowData$
    ]).pipe(debounceTime(1000))
    .subscribe({
      next: ([report, teeth]) => {
        this.clientFormGroup.controls.name.setValue(`${report.clientModel.name} ${report.clientModel.middleName} ${report.clientModel.surname} ${report.clientModel.secondSurname}`)
        this.clientFormGroup.controls.email.setValue(report.clientModel.email)
        this.clientFormGroup.controls.document.setValue(report.clientModel.document)
        this.clientFormGroup.controls.ruc.setValue(report.clientModel.ruc)
        this.clientFormGroup.controls.country.setValue(report.clientModel.country)
        this.clientFormGroup.controls.phone.setValue(report.clientModel.phone)
        this.invoiceDetailReportApiModelList = report.invoiceModels.flatMap(x => x.invoiceDetails)
        this.invoiceDetailReportApiModelList.forEach(x => x.displayTeeth = x.toothIds && x.toothIds.length > 0 ?
          x.toothIds?.map(x => teeth.find(y => y.id === x)!.number).sort().join(', ') :
          'Sin dientes'
        )
        this.total = report.invoiceModels.reduce((a, b) => a + b.total, 0)
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

  protected printReport =() => {
    print()
  }
}
