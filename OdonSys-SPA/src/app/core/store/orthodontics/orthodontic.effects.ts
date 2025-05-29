import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { catchError, map, of, switchMap, tap, withLatestFrom } from 'rxjs';
import { SubscriptionService } from '../../services/shared/subscription.service';
import { OrthodonticApiService } from '../../services/api/orthodontic-api.service';
import { AlertService } from '../../services/shared/alert.service';
import { OrthodonticActions } from './orthodontic.actions';
import { selectOrthodontics } from './orthodontic.selectors';
import { OrthodonticApiModel } from '../../models/api/orthodontics/orthodontic-api-model';
import { OrthodonticModel } from '../../models/view/orthodontic-model';
import { ClientApiModel } from '../../models/api/clients/client-api-model';
import { ClientModel } from '../../models/view/client-model';

@Injectable()
export class OrthodonticEffects {
  constructor(
    private actions$: Actions,
    private readonly orthodonticApiService: OrthodonticApiService,
    private readonly store: Store,
    private readonly router: Router,
    private readonly alertService: AlertService,
    private readonly subscriptionService: SubscriptionService
  ) {}

  getAll$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(OrthodonticActions.loadOrthodontics),
      withLatestFrom(this.store.select(selectOrthodontics)),
      switchMap(([_, orthodontics]) => orthodontics.length > 0 ?
      of(OrthodonticActions.addOrthodontics({ orthodontics: orthodontics })) :
        this.orthodonticApiService.getAll().pipe(
          map(data => OrthodonticActions.addOrthodontics({ orthodontics: data.map(this.getModel) })),
          catchError(error => of(OrthodonticActions.failure({ error })))
        )
      )
    )
  })

  create$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(OrthodonticActions.addOrthodontic),
      switchMap((action) =>
        this.orthodonticApiService.create(action.orthodontic).pipe(
          map(data => {
            this.router.navigate([action.redirectUrl])
            this.alertService.showSuccess('Ortodoncia registrado con éxito.')
            return OrthodonticActions.addOrthodonticSuccess({ orthodontic: this.getModel(data) })
          }),
          catchError(error => of(OrthodonticActions.failure({ error })))
        )
      )
    )
  })

  update$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(OrthodonticActions.updateOrthodontic),
      switchMap((action) =>
        this.orthodonticApiService.update(action.id, action.orthodontic).pipe(
          map(data => {
            this.router.navigate([action.redirectUrl])
            this.alertService.showSuccess('Ortodoncia actualizado con éxito.')
            return OrthodonticActions.updateOrthodonticSuccess({ orthodontic: this.getModel(data) })
          }),
          catchError(error => of(OrthodonticActions.failure({ error })))
        )
      )
    )
  })

  delete$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(OrthodonticActions.deleteOrthodontic),
      switchMap((x) =>
        this.orthodonticApiService.delete(x.id).pipe(
          map(data => {
          this.alertService.showSuccess(`Ortodoncia actualizado con éxito.`)
          return OrthodonticActions.deleteOrthodontic({ id: data.id})
        }),
        catchError(error => of(OrthodonticActions.failure({ error }))))
      )
    )
  })

  private errorHandler$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(OrthodonticActions.failure),
      tap((x) => {
        this.subscriptionService.emitErrorInSave()
        throw x.error
      })
    )
  })

  private getModel = (data: OrthodonticApiModel) => new OrthodonticModel(
    data.id,
    data.date,
    data.dateCreated,
    data.dateModified,
    data.description,
    this.getClientModel(data.client)
  )

  private getClientModel = (data: ClientApiModel) => new ClientModel(
    data.id,
    data.active,
    data.dateCreated,
    data.dateModified,
    data.name,
    data.middleName,
    data.surname,
    data.secondSurname,
    data.document,
    data.ruc,
    data.country,
    data.debts,
    data.phone,
    data.email,
    data.doctors
  )
}
