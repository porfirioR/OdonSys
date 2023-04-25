import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { catchError, map, of, switchMap, tap, withLatestFrom } from 'rxjs';
import * as procedureActions from './procedure.actions';
import { ProcedureModel } from '../../models/procedure/procedure-model';
import { ProcedureApiService } from '../../services/api/procedure-admin-api.service';
import { AlertService } from '../../services/shared/alert.service';
import { selectProcedures } from './procedure.selectors';

@Injectable()
export class ProcedureEffects {

  constructor(
    private actions$: Actions,
    private readonly procedureApiService: ProcedureApiService,
    private readonly store: Store,
    private readonly router: Router,
    private readonly alertService: AlertService
    ) {}

  getAll$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(procedureActions.loadProcedures),
      withLatestFrom(this.store.select(selectProcedures)),
      switchMap(([action, procedures]) => procedures.length > 0 ?
      of(procedureActions.allProceduresLoaded({ procedures: procedures })) :
        this.procedureApiService.getAll().pipe(
          map(data => procedureActions.allProceduresLoaded({
            procedures: data.map(x => 
              new ProcedureModel(
                x.id,
                x.active,
                x.dateCreated,
                x.dateModified,
                x.name,
                x.description,
                x.procedureTeeth,
                x.price
              )
            )
          })),
          catchError(error => of(procedureActions.procedureFailure({ error })))
        )
      )
    )
  })

  create$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(procedureActions.addProcedure),
      switchMap((action) =>
        this.procedureApiService.create(action.procedure).pipe(
          map(data => {
            this.router.navigate(['/admin/procedimientos'])
            this.alertService.showSuccess('Tratamiento creado con éxito.')
            return procedureActions.addProcedureSuccess({
              procedure: new ProcedureModel(
                data.id,
                data.active,
                data.dateCreated,
                data.dateModified,
                data.name,
                data.description,
                data.procedureTeeth,
                data.price
              )
            })
          }),
          catchError(error => of(procedureActions.procedureFailure({ error })))
        )
      )
    )
  })

  update$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(procedureActions.updateProcedure),
      switchMap((action) =>
        this.procedureApiService.update(action.procedure).pipe(
          map(data => {
            this.router.navigate(['/admin/procedimientos'])
            this.alertService.showSuccess('Tratamiento actualizado con éxito.')
            return procedureActions.updateProcedureSuccess({
              procedure: new ProcedureModel(
                data.id,
                data.active,
                data.dateCreated,
                data.dateModified,
                data.name,
                data.description,
                data.procedureTeeth,
                data.price
              )
            })
          }),
          catchError(error => of(procedureActions.procedureFailure({ error })))
        )
      )
    )
  })

  patchProcedure$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(procedureActions.changeProcedureVisibility),
      switchMap((x) =>
        this.procedureApiService.changeVisibility(x.id, x.model).pipe(
          map(data => {
          this.alertService.showSuccess(`Tratamiento fue ${data.active ? 'restaurado' : 'deshabilitado'} con éxito.`)
          return procedureActions.changeProcedureVisibilitySuccess({
            procedure: new ProcedureModel(
              data.id,
              data.active,
              data.dateCreated,
              data.dateModified,
              data.name,
              data.description,
              data.procedureTeeth,
              data.price
            )
          })
        }),
        catchError(error => of(procedureActions.procedureFailure({ error }))))
      )
    )
  })

  errorHandler$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(procedureActions.procedureFailure),
      tap((x) => {throw x.error})
    )
  })
}
