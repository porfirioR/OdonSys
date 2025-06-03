import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { combineLatest, Observable, of } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { SelectModel } from '../../../core/models/view/select-model';
import { selectOrthodontics } from '../../../core/store/orthodontics/orthodontic.selectors';
import { savingSelector } from '../../../core/store/saving/saving.selector';
import { OrthodonticActions } from '../../../core/store/orthodontics/orthodontic.actions';
import { SubscriptionService } from '../../../core/services/shared/subscription.service';
import { OrthodonticFormGroup } from '../../../core/forms/orthodontic-form-group.form';
import { OrthodonticRequest } from '../../../core/models/api/orthodontics/orthodontic-request';
import { selectClients } from '../../../core/store/clients/client.selectors';
import  * as fromClientsActions from '../../../core/store/clients/client.actions';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-upsert-orthodontic',
  templateUrl: './upsert-orthodontic.component.html',
  styleUrls: ['./upsert-orthodontic.component.scss'],
  providers: [DatePipe]
})
export class UpsertOrthodonticComponent implements OnInit {
  public formGroup = new FormGroup<OrthodonticFormGroup>({
    description: new FormControl('', [Validators.required, Validators.maxLength(250)]),
    clientId: new FormControl('', [Validators.required]),
    date: new FormControl(null, [Validators.required])
  })
  public saving = false
  public ignorePreventUnsavedChanges = false
  protected title = 'Registrar '
  protected saving$: Observable<boolean> = of(false)
  protected clientValues: SelectModel[] = []
  private id = ''
  private clientId = ''
  private allOrthodonticsUrl = '/trabajo/ortodoncias'
  private myOrthodonticsUrlUrl = '/trabajo/mis-ortodoncias'

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly router: Router,
    private store: Store,
    private readonly subscriptionService: SubscriptionService,
    private datePipe: DatePipe,
  ) { }

  ngOnInit(): void {
    this.saving$ = this.store.select(savingSelector)
    this.subscriptionService.onErrorInSave.subscribe({
      next: (): void => {
        this.saving = false
        this.ignorePreventUnsavedChanges = false
      }
    })
    this.loadValues()
  }

  protected close = (): void => {
    this.router.navigate([this.redirectUrl()])
  }

  protected save = (): void => {
    if (this.formGroup.invalid) { return }
    this.saving = true
    this.ignorePreventUnsavedChanges = true
    this.upsert()
  }

  private loadValues = (): void => {
    this.id = this.activatedRoute.snapshot.params['id']
    this.clientId = this.activatedRoute.snapshot.params['clientId']
    const isUpdateUrl = this.activatedRoute.snapshot.url[1].path === 'actualizar'
    let loading = true
    const clients$ = this.store.select(selectClients).pipe(tap(x => {
      if(loading && x.length === 0) {
        this.store.dispatch(fromClientsActions.loadClients()) 
        loading = false
      }
    }))
    const orthodontic$ = this.store.pipe(
      select(selectOrthodontics),
      map(x => this.id ? x.find(y => y.id === this.id) ?? undefined : undefined)
    )
    combineLatest([clients$, orthodontic$]).subscribe({
      next: ([clients, orthodontic]) => {
        clients.forEach(x => this.clientValues.push(new SelectModel(x.id, `${x.name} ${x.surname}`)))
        if (isUpdateUrl && !orthodontic) {
          this.router.navigate([`${this.redirectUrl()}/registrar`])
        }
        if (this.id && orthodontic) {
          this.title = 'Actualizar '
          const date = this.datePipe.transform(orthodontic.date, 'yyyy-MM-dd') as unknown as Date
          this.formGroup.patchValue({
            clientId: orthodontic.client.id,
            description: orthodontic.description,
            date: date
          })
        }
        if (this.clientId) {
          this.formGroup.patchValue({
            clientId: this.clientId
          })
        }
      }
    })
  }

  private upsert = (): void => {
    const request = new OrthodonticRequest(
      this.formGroup.value.clientId!,
      this.formGroup.value.description!,
      this.formGroup.value.date!
    )
    if (this.id) {
      const store = {
        id: this.id,
        orthodontic: request,
        redirectUrl: this.redirectUrl()
      }
      this.store.dispatch(OrthodonticActions.updateOrthodontic(store))
    } else {
      const store = {
        orthodontic: request,
        redirectUrl: this.redirectUrl()
      }
      this.store.dispatch(OrthodonticActions.addOrthodontic(store))
    }
  }

  private redirectUrl = (): string => {
    return this.router.url.startsWith(this.allOrthodonticsUrl) ? this.allOrthodonticsUrl : `${this.myOrthodonticsUrlUrl}/${this.id}`
  }
}
