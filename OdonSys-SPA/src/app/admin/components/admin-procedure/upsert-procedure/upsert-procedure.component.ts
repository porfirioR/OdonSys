import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { of, throwError } from 'rxjs';
import { catchError, first, switchMap, tap } from 'rxjs/operators';
import { ProcedureApiService } from '../../../../admin/service/procedure-admin-api.service';
import { ProcedureApiModel } from '../../../../core/models/procedure/procedure-api-model';
import { AlertService } from '../../../../core/services/shared/alert.service';

@Component({
  selector: 'app-upsert-procedure',
  templateUrl: './upsert-procedure.component.html',
  styleUrls: ['./upsert-procedure.component.scss']
})
export class UpsertProcedureComponent implements OnInit {
  public load: boolean = false;
  public title = 'crear';
  private id = '';
  public formGroup: FormGroup = new FormGroup({});

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly router: Router,
    private readonly alertService: AlertService,
    private readonly procedureApiService: ProcedureApiService,

  ) { }

  ngOnInit() {
    this.loadValues();
  }

  public save = () => {

  }

  public close = () => {
    this.router.navigate(['admin/procedimentos']);
  }

  private loadValues = () => {
    this.activatedRoute.params.pipe(switchMap(params => {
      this.id = params.id;
      const procedure$ = this.id ? this.procedureApiService.getById(this.id) : of(new ProcedureApiModel());
      return procedure$.pipe(
        first(),
        tap(x => {
          this.formGroup = new FormGroup( {
            name : new FormControl(this.id ? x.name: '', [Validators.required, Validators.maxLength(30)]),
            description : new FormControl(this.id ? x.description: '', [Validators.required, Validators.maxLength(50)]),
            estimatedSessions : new FormControl(this.id ? x.estimatedSessions: '', [Validators.required, Validators.maxLength(50)])
          })
          this.load = true;
        }),
        catchError(e => {
          this.load = true;
          return throwError(e);
        }));
    })).subscribe()
  }
}
