import { createReducer, on } from '@ngrx/store';
import { EntityAdapter, EntityState, createEntityAdapter } from '@ngrx/entity';
import * as RolesActions from './roles.actions';
import { RoleModel } from '../../models/view/role-model';

export const roleFeatureKey = 'roles'

export interface RoleState extends EntityState<RoleModel> { }

export const adapter: EntityAdapter<RoleModel> = createEntityAdapter<RoleModel>({
  selectId: selectRoleId,
  sortComparer: sortByCode
});

export const initialState: RoleState = adapter.getInitialState({ })

export function selectRoleId(a: RoleModel): string {
  return a.code
}

export function sortByCode(a: RoleModel, b: RoleModel): number {
  return a.code.localeCompare(b.code)
}

export const reducer = createReducer(
  initialState,
  on(RolesActions.allRolesLoaded,
    (state, action) => adapter.setAll(action.roles, state)
  ),
  on(RolesActions.createRoleSuccess,
    (state, action) => adapter.setOne(action.role, state),
  ),
  on(RolesActions.updateRoleSuccess,
    (state, action) => adapter.upsertOne(action.role, state),
  ),
  on(RolesActions.clearRoles,
    state => adapter.removeAll(state)
  )
)

export const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal
} = adapter.getSelectors()
