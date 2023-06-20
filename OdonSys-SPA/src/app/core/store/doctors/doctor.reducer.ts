import { createReducer, on } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import * as DoctorActions from './doctor.actions';
import { DoctorModel } from '../../models/view/doctor-model';

export const doctorsFeatureKey = 'doctors';

export interface DoctorState extends EntityState<DoctorModel> { }

export const adapter: EntityAdapter<DoctorModel> = createEntityAdapter<DoctorModel>();

export const initialState: DoctorState = adapter.getInitialState({ })

export const reducer = createReducer(
  initialState,
  on(DoctorActions.allDoctorsLoaded,
    (state, action) => adapter.setAll(action.doctors, state)
  ),
  on(
    DoctorActions.changeDoctorVisibilitySuccess,
    DoctorActions.updateDoctorSuccess,
    (state, action) => adapter.upsertOne(action.doctor, state)
  ),
  on(DoctorActions.clearDoctors,
    state => adapter.removeAll(state)
  ),
  on(DoctorActions.updateDoctorSuccess,
    (state, action) => adapter.setOne(action.doctor, state)
  )
)

export const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
} = adapter.getSelectors()
