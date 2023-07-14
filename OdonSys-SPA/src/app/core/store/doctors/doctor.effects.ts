import { Injectable, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { catchError, map, of, switchMap, tap, withLatestFrom } from 'rxjs';
import { AlertService } from '../../services/shared/alert.service';
import { DoctorApiService } from '../../services/api/doctor-api.service';
import { SubscriptionService } from '../../services/shared/subscription.service';
import { UserApiService } from '../../services/api/user-api.service';
import * as doctorActions from './doctor.actions';
import { selectDoctors } from './doctor.selectors';
import { DoctorApiModel } from '../../models/api/doctor/doctor-api-model';
import { DoctorModel } from '../../models/view/doctor-model';
import { UserApiModel } from '../../models/users/api/user-api-model';
import { RoleApiService } from '../../services/api/role-api.service';
import { UserInfoService } from '../../services/shared/user-info.service';

@Injectable()
export class DoctorEffects {
  constructor(
    private actions$: Actions,
    private readonly store: Store,
    private readonly alertService: AlertService,
    private readonly subscriptionService: SubscriptionService,
    private readonly userApiService: UserApiService,
    private readonly doctorApiService: DoctorApiService,
    private readonly zone: NgZone,
    private readonly router: Router,
    private readonly roleApiService: RoleApiService,
    private readonly userInfoService: UserInfoService,
  ) {}

  protected getAll$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(doctorActions.loadDoctors),
      withLatestFrom(this.store.select(selectDoctors)),
      switchMap(([_, doctors]) => doctors.length > 1 ?
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
        const doctor = doctors.find(x => x.id.compareString(action.doctorId))
        if (doctors.length > 0 && !!doctor) {
          return of(doctorActions.loadDoctorSuccess({ doctor: doctor! }))
        }
        return this.doctorApiService.getById(action.doctorId).pipe(
          map((x) => doctorActions.loadDoctorSuccess({ doctor: this.getModel(x) })),
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
      withLatestFrom(this.store.select(selectDoctors)),
      switchMap(([action, doctors]) =>
        this.userApiService.approve(action.doctorId).pipe(
          map((data: UserApiModel) => {
            this.alertService.showSuccess(`El doctor ha sido habilitado para ingresar al sistema.`)
            let doctor = {...doctors.find(x => x.id.compareString(data.id))!}
            doctor.approved = data.approved
            return doctorActions.approveDoctorSuccess({ doctor: this.getModel(doctor) })
          }),
          catchError(error => of(doctorActions.doctorFailure({ error })))
        )
      )
    )
  })

  protected updateDoctorRoles$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(doctorActions.updateDoctorRoles),
      switchMap((action) => 
        this.roleApiService.getMyPermissions().pipe(map(permissions => {
          let doctor = {...action.doctor}
          if (action.doctorRoles) {
            doctor.roles = action.doctorRoles
          }
          this.userInfoService.setRoles(doctor.roles)
          this.userInfoService.setUserPermissions(permissions)
          return doctorActions.updateDoctorSuccess({ doctor: doctor })
        }))
      )
    )
  })

  protected updateDoctor$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(doctorActions.updateDoctor),
      switchMap((x) => this.doctorApiService.update(x.user).pipe(
        map((data: DoctorApiModel) => {
          this.zone.run(() => this.router.navigate(['']))
          this.alertService.showSuccess('Datos actualizados correctamente.')
          return doctorActions.updateDoctorSuccess({ doctor: this.getModel(data) })
        }),
        catchError(error => of(doctorActions.doctorFailure({ error })))
      ))
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
    data.id.toUpperCase(),
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
