import { createAction, props } from '@ngrx/store';
import { HttpErrorResponse } from '@angular/common/http';
import { DoctorModel } from '../../models/view/doctor-model';
import { PatchRequest } from '../../models/api/patch-request';

export const loadDoctors = createAction(
  '[Doctors Component] Load Doctors'
)

export const allDoctorsLoaded = createAction(
  '[Doctor Effect] Load Doctors',
  props<{ doctors: DoctorModel[] }>()
)

export const approveDoctor = createAction(
  '[Doctors Component] Approve Doctor',
  props<{
    doctorId: string
  }>()
)

export const approveDoctorSuccess = createAction(
  '[Doctor Effect] Approve Doctor Success',
  props<{ doctor: DoctorModel }>()
)

export const updateDoctor = createAction(
  '[Doctor Component] Update Doctor',
  props<{
    doctor: DoctorModel,
    doctorRoles?: string[]
  }>()
)

export const changeDoctorVisibility = createAction(
  '[Doctor Component] Change Visibility Doctors',
  props<{
    id: string,
    model: PatchRequest
  }>()
)

export const changeDoctorVisibilitySuccess = createAction(
  '[Doctor Effect] Change Visibility Doctor Success',
  props<{ doctor: DoctorModel }>()
)

export const updateDoctorSuccess = createAction(
  '[Doctor Effect] Update Doctor Success',
  props<{ doctor: DoctorModel }>()
)

export const doctorFailure = createAction(
  '[Doctor] Doctors Failure',
  props<{ error: HttpErrorResponse }>()
)

export const clearDoctors = createAction(
  '[Doctor] Clear Doctors'
)
