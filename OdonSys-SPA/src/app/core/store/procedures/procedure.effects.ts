import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { catchError, map, of, switchMap, tap, withLatestFrom } from 'rxjs';
import * as procedureActions from './procedure.actions';
import { ProcedureModel } from '../../models/procedure/procedure-model';
import { ProcedureApiModel } from '../../models/procedure/procedure-api-model';
import { ProcedureApiService } from '../../services/api/procedure-admin-api.service';
import { AlertService } from '../../services/shared/alert.service';
import { SubscriptionService } from '../../services/shared/subscription.service';
import { selectProcedures } from './procedure.selectors';

@Injectable()
export class ProcedureEffects {

  constructor(
    private actions$: Actions,
    private readonly procedureApiService: ProcedureApiService,
    private readonly store: Store,
    private readonly router: Router,
    private readonly alertService: AlertService,
    private readonly subscriptionService: SubscriptionService
  ) {}

  protected getAll$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(procedureActions.loadProcedures),
      withLatestFrom(this.store.select(selectProcedures)),
      switchMap(([_, procedures]) => procedures.length > 0 ?
      of(procedureActions.allProceduresLoaded({ procedures: procedures })) :
        this.procedureApiService.getAll().pipe(
          map(data => procedureActions.allProceduresLoaded({ procedures: data.map(this.getModel) })),
          catchError(error => of(procedureActions.procedureFailure({ error })))
        )
      )
    )
  })

  protected create$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(procedureActions.addProcedure),
      switchMap((action) =>
        this.procedureApiService.create(action.procedure).pipe(
          map(data => {
            this.router.navigate(['/admin/procedimientos'])
            this.alertService.showSuccess('Tratamiento creado con éxito.')
            return procedureActions.addProcedureSuccess({
              procedure: this.getModel(data)
            })
          }),
          catchError(error => of(procedureActions.procedureFailure({ error })))
        )
      )
    )
  })

  protected update$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(procedureActions.updateProcedure),
      switchMap((action) =>
        this.procedureApiService.update(action.procedure).pipe(
          map(data => {
            this.router.navigate(['/admin/procedimientos'])
            this.alertService.showSuccess('Tratamiento actualizado con éxito.')
            return procedureActions.updateProcedureSuccess({
              procedure: this.getModel(data)
            })
          }),
          catchError(error => of(procedureActions.procedureFailure({ error })))
        )
      )
    )
  })

  protected patchProcedure$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(procedureActions.changeProcedureVisibility),
      switchMap((x) =>
        this.procedureApiService.changeVisibility(x.id, x.model).pipe(
          map(data => {
          this.alertService.showSuccess(`Tratamiento fue ${data.active ? 'restaurado' : 'deshabilitado'} con éxito.`)
          return procedureActions.changeProcedureVisibilitySuccess({
            procedure: this.getModel(data)
          })
        }),
        catchError(error => of(procedureActions.procedureFailure({ error }))))
      )
    )
  })

  private errorHandler$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(procedureActions.procedureFailure),
      tap((x) => {
        this.subscriptionService.emitErrorInSave()
        throw x.error
      })
    )
  })

  private getModel = (data: ProcedureApiModel) => new ProcedureModel(
    data.id,
    data.active,
    data.dateCreated,
    data.dateModified,
    data.name,
    data.description,
    data.procedureTeeth,
    data.price,
    data.xRay
  )
}
