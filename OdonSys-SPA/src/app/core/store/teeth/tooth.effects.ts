import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { catchError, map, of, switchMap, tap, withLatestFrom } from 'rxjs';
import * as toothActions from './tooth.actions';
import { selectTeeth } from './tooth.selectors';
import { ToothApiService } from '../../services/api/tooth-api.service';
import { SubscriptionService } from '../../services/shared/subscription.service';

@Injectable()
export class ToothEffects {
  constructor(
    private actions$: Actions,
    private readonly store: Store,
    private readonly toothApiService: ToothApiService,
    private readonly subscriptionService: SubscriptionService,
  ) {}

  protected getAll$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(toothActions.componentLoadTeeth),
      withLatestFrom(this.store.select(selectTeeth)),
      switchMap(([_, teeth]) => teeth.length > 1 ?
        of(toothActions.effectAllTeethLoaded({ teeth: teeth })) :
        this.toothApiService.getAll().pipe(
          map(data => toothActions.effectAllTeethLoaded({ teeth: data })),
          catchError(error => of(toothActions.failureToLoadTeeth({ error })))
        )
      )
    )
  })

  private errorHandler$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(toothActions.failureToLoadTeeth),
      tap((x) => {
        this.subscriptionService.emitErrorInSave()
        throw x.error
      })
    )
  })

}
