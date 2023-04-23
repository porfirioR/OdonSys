import { createAction, props } from '@ngrx/store';
import { HttpErrorResponse } from '@angular/common/http';
import { ProcedureModel } from '../../models/procedure/procedure-model';
import { UpdateProcedureRequest } from '../../models/procedure/update-procedure-request';
import { CreateProcedureRequest } from '../../models/procedure/create-procedure-request';

export const loadProcedures = createAction(
  '[Procedure Component] Load Procedures'
)

export const allProceduresLoaded = createAction(
  '[Procedure Effect] Load Roles',
  props<{ procedures: ProcedureModel[] }>()
)

export const addProcedure = createAction(
  '[Procedure Component] Add Procedure',
  props<{ procedure: CreateProcedureRequest }>()
)

export const addProcedureSuccess = createAction(
  '[Procedure Effects] Add Procedure Success',
  props<{ procedure: ProcedureModel }>()
)

export const updateProcedureSuccess = createAction(
  '[Procedure Effects] Update Procedure Success',
  props<{ procedure: ProcedureModel }>()
)

export const updateProcedure = createAction(
  '[Procedure Component] Update Procedure',
  props<{ procedure: UpdateProcedureRequest }>()
)

export const deleteProcedure = createAction(
  '[Procedure/API] Delete Procedure',
  props<{ id: string }>()
)

export const clearProcedures = createAction(
  '[Procedure/API] Clear Procedures'
)

export const procedureFailure = createAction(
  '[Procedure] Get Procedures Failure',
  props<{ error: HttpErrorResponse }>()
)