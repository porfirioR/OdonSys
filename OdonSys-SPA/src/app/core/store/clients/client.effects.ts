import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { catchError, map, of, switchMap, tap, withLatestFrom } from 'rxjs';
import { AlertService } from '../../services/shared/alert.service';
import { ClientAdminApiService } from '../../services/api/client-admin-api.service';
import * as clientActions from './client.actions';
import { selectClients } from './client.selectors';
import { ClientModel } from '../../models/view/client-model';
import { ClientApiModel } from '../../models/api/clients/client-api-model';
import { SubscriptionService } from '../../services/shared/subscription.service';

@Injectable()
export class ClientEffects {
  constructor(
    private actions$: Actions,
    private readonly clientApiService: ClientAdminApiService,
    private readonly store: Store,
    private readonly router: Router,
    private readonly alertService: AlertService,
    private readonly subscriptionService: SubscriptionService
  ) {}

  getAll$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(clientActions.loadClients),
      withLatestFrom(this.store.select(selectClients)),
      switchMap(([action, clients]) => clients.length > 0 ?
      of(clientActions.allClientsLoaded({ clients: clients })) :
        this.clientApiService.getAll().pipe(
          map(data => clientActions.allClientsLoaded({ clients: data.map(this.getModel) })),
          catchError(error => of(clientActions.clientFailure({ error })))
        )
      )
    )
  })

  create$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(clientActions.addClient),
      switchMap((action) =>
        this.clientApiService.createClient(action.client).pipe(
          map(data => {
            this.router.navigate(['/admin/pacientes'])
            this.alertService.showSuccess('Paciente registrado con éxito.')
            return clientActions.addClientSuccess({ client: this.getModel(data) })
          }),
          catchError(error => of(clientActions.clientFailure({ error })))
        )
      )
    )
  })

  update$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(clientActions.updateClient),
      switchMap((action) =>
        this.clientApiService.updateClient(action.client).pipe(
          map(data => {
            this.router.navigate(['/admin/pacientes'])
            this.alertService.showSuccess('Paciente actualizado con éxito.')
            return clientActions.updateClientSuccess({ client: this.getModel(data) })
          }),
          catchError(error => of(clientActions.clientFailure({ error })))
        )
      )
    )
  })

  patchClient$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(clientActions.changeClientVisibility),
      switchMap((x) =>
        this.clientApiService.changeVisibility(x.id, x.model).pipe(
          map(data => {
          this.alertService.showSuccess(`Paciente fue ${data.active ? 'restaurado' : 'deshabilitado'} con éxito.`)
          return clientActions.changeClientVisibilitySuccess({ client: this.getModel(data) })
        }),
        catchError(error => of(clientActions.clientFailure({ error }))))
      )
    )
  })

  errorHandler$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(clientActions.clientFailure),
      tap((x) => {
        this.subscriptionService.emitErrorInSave()
        throw x.error
      })
    )
  })

  private getModel = (data: ClientApiModel) => new ClientModel(
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
