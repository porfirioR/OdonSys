import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { catchError, map, mergeMap, of, switchMap, tap, withLatestFrom } from 'rxjs';
import { AlertService } from '../../services/shared/alert.service';
import { DoctorApiService } from '../../services/api/doctor-api.service';
import { SubscriptionService } from '../../services/shared/subscription.service';
import { UserApiService } from '../../services/api/user-api.service';
import * as doctorActions from './doctor.actions';
import { selectDoctors } from './doctor.selectors';
import { DoctorApiModel } from '../../models/api/doctor/doctor-api-model';
import { DoctorModel } from '../../models/view/doctor-model';

@Injectable()
export class DoctorEffects {
  constructor(
    private actions$: Actions,
    private readonly store: Store,
    private readonly alertService: AlertService,
    private readonly subscriptionService: SubscriptionService,
    private readonly userApiService: UserApiService,
    private readonly doctorApiService: DoctorApiService,
  ) {}

  protected getAll$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(doctorActions.loadDoctors),
      withLatestFrom(this.store.select(selectDoctors)),
      switchMap(([_, doctors]) => doctors.length > 0 ?
      of(doctorActions.allDoctorsLoaded({ doctors: doctors })) :
        this.userApiService.getAll().pipe(
          map(data => doctorActions.allDoctorsLoaded({ doctors: data.map(this.getModel) })),
          catchError(error => of(doctorActions.doctorFailure({ error })))
        )
      )
    )
  })

  protected getDoctorById$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(doctorActions.loadDoctor),
      withLatestFrom(this.store.select(selectDoctors)),
      switchMap(([action, doctors]) => {
        const doctor = doctors.find(x => x.id === action.doctorId)
        if (doctors.length > 0 && !!doctor) {
          return of(doctorActions.loadDoctorSuccess({ doctor: doctor! }))
        }
        return this.doctorApiService.getById(action.doctorId).pipe(
          map((doctor) => doctorActions.loadDoctorSuccess({ doctor: this.getModel(doctor) })),
          catchError(error => of(doctorActions.doctorFailure({ error })))
        )
      })
    )
  })

  protected patchDoctor$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(doctorActions.changeDoctorVisibility),
      switchMap((x) =>
        this.userApiService.changeVisibility(x.id, x.model).pipe(
          map(data => {
            this.alertService.showSuccess(`Doctor fue ${data.active ? 'restaurado' : 'deshabilitado'} con éxito.`)
            return doctorActions.changeDoctorVisibilitySuccess({ doctor: this.getModel(data) })
          }),
          catchError(error => of(doctorActions.doctorFailure({ error })))
        )
      )
    )
  })

  protected approveDoctor$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(doctorActions.approveDoctor),
      switchMap((x) =>
        this.userApiService.approve(x.doctorId).pipe(
          map((data: DoctorApiModel) => {
            this.alertService.showSuccess(`El doctor ha sido habilitado para ingresar al sistema.`)
            return doctorActions.approveDoctorSuccess({ doctor: this.getModel(data) })
          }),
          catchError(error => of(doctorActions.doctorFailure({ error })))
        )
      )
    )
  })

  protected updateDoctor$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(doctorActions.updateDoctor),
      map((x) => {
        let doctor = {...x.doctor}
        if (x.doctorRoles) {
          doctor.roles = x.doctorRoles
        }
        return doctorActions.updateDoctorSuccess({ doctor: doctor })
      })
    )
  })

  private errorHandler$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(doctorActions.doctorFailure),
      tap((x) => {
        this.subscriptionService.emitErrorInSave()
        throw x.error
      })
    )
  })

  private getModel = (data: DoctorApiModel) => new DoctorModel(
    data.id,
    data.name,
    data.middleName,
    data.surname,
    data.secondSurname,
    data.document,
    data.country,
    data.email,
    data.phone,
    data.userName,
    data.active,
    data.approved,
    data.roles
  )
}
