import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromProcedure from './procedure.reducer'
export const selectRolesFeature = createFeatureSelector<fromProcedure.ProcedureState>(fromProcedure.proceduresFeatureKey);

export const selectProcedures = createSelector(
  selectRolesFeature,
  fromProcedure.selectAll
)
