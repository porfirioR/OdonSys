import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, of, throwError } from 'rxjs';
import { catchError, switchMap, tap } from 'rxjs/operators';
import { ToothApiService } from '../../../../core/services/api/tooth-api.service';
import { ProcedureApiService } from '../../../../admin/service/procedure-admin-api.service';
import { ProcedureApiModel } from '../../../../core/models/procedure/procedure-api-model';
import { AlertService } from '../../../../core/services/shared/alert.service';
import { ToothModel } from 'src/app/core/models/tooth/tooth-model';

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
  public teethList: ToothModel[] = [];

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly router: Router,
    private readonly alertService: AlertService,
    private readonly procedureApiService: ProcedureApiService,
    private readonly toothApiService: ToothApiService,

  ) { }

  ngOnInit() {
    this.loadValues();
  }

  public save = () => {

  }

  public close = () => {
    this.router.navigate(['admin/procedimientos']);
  }

  private loadValues = () => {
    this.activatedRoute.params.pipe(switchMap(params => {
      this.id = params.id;
      const procedure$ = this.id ? this.procedureApiService.getById(this.id) : of(new ProcedureApiModel());
      return forkJoin([procedure$, this.toothApiService.getAll()]).pipe(
        tap(([procedure, teethList]) => {
          this.formGroup = new FormGroup( {
            name : new FormControl(this.id ? procedure.name: '', [Validators.required, Validators.maxLength(30)]),
            description : new FormControl(this.id ? procedure.description: '', [Validators.required, Validators.maxLength(50)]),
            estimatedSessions : new FormControl(this.id ? procedure.estimatedSessions: '', [Validators.required, Validators.maxLength(50)]),
            active : new FormControl(this.id ? procedure.active: true, [Validators.required])
          })
          if (!this.id) {
            this.formGroup.controls.active.disable();
          }
          this.teethList = Object.assign([], teethList);
          this.load = true;
        }),
        catchError(e => {
          this.load = true;
          return throwError(e);
        }));
    })).subscribe()
  }
}
