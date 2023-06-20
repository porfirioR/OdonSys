import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromDoctor from './doctor.reducer';

export const selectDoctorsFeature = createFeatureSelector<fromDoctor.DoctorState>(fromDoctor.doctorsFeatureKey);

export const selectDoctors = createSelector(
  selectDoctorsFeature,
  fromDoctor.selectAll
)

export const selectDoctor = (doctorId: string) => createSelector(
  selectDoctors,
  (doctors) => doctors.find(x => x.id === doctorId)
)

