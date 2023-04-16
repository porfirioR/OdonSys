import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromRole from './roles.reducer'
export const selectRolesFeature = createFeatureSelector<fromRole.RoleState>(fromRole.roleFeatureKey);

export const selectRoles = createSelector(
  selectRolesFeature,
  fromRole.selectAll
)
