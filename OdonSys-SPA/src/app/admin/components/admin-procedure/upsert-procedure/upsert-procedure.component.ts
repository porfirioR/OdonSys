import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { of, throwError } from 'rxjs';
import { catchError, first, switchMap, tap } from 'rxjs/operators';
import { ProcedureApiService } from 'src/app/admin/service/procedure-admin-api.service';
import { ProcedureApiModel } from 'src/app/core/models/procedure/procedure-api-model';
import { AlertService } from 'src/app/core/services/shared/alert.service';

@Component({
  selector: 'app-upsert-procedure',
  templateUrl: './upsert-procedure.component.html',
  styleUrls: ['./upsert-procedure.component.scss']
})
export class UpsertProcedureComponent implements OnInit {
  public load: boolean = false;
  public title = 'crear';
  private id = '';

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly alertService: AlertService,
    private readonly procedureApiService: ProcedureApiService,

  ) { }

  ngOnInit() {
    this.loadValues();
  }

  private loadValues = () => {
    this.activatedRoute.params.pipe(switchMap(params => {
      this.id = params.id;
      const procedure$ = this.id ? this.procedureApiService.getById(this.id) : of(new ProcedureApiModel());
      return procedure$.pipe(
        first(),
        tap(x => {
          alert('hi');
          this.load = true;
        }),
        catchError(e => {
          this.load = true;
          return throwError(e);
        }));
    })).subscribe()
  }
}
