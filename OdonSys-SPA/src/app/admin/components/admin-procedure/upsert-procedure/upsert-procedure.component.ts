import { Component, OnInit } from '@angular/core';
import { AbstractControl, UntypedFormArray, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { ToothApiService } from '../../../../core/services/api/tooth-api.service';
import { ProcedureApiService } from '../../../../admin/service/procedure-admin-api.service';
import { ProcedureApiModel } from '../../../../core/models/procedure/procedure-api-model';
import { AlertService } from '../../../../core/services/shared/alert.service';
import { ToothModel } from '../../../../core/models/tooth/tooth-model';
import { Jaw } from '../../../../core/enums/jaw.enum';
import { Quadrant } from '../../../../core/enums/quadrant.enum';
import { ToothApiModel } from '../../../../core/models/tooth/tooth-api-model';
import { CreateProcedureRequest } from '../../../../admin/models/procedure/api/create-procedure-request';
import { UpdateProcedureRequest } from '../../../../admin/models/procedure/api/update-procedure-request';

@Component({
  selector: 'app-upsert-procedure',
  templateUrl: './upsert-procedure.component.html',
  styleUrls: ['./upsert-procedure.component.scss']
})
export class UpsertProcedureComponent implements OnInit {
  public load: boolean = false;
  public saving: boolean = false;
  public title = 'crear';
  private id = '';
  public formGroup: FormGroup = new FormGroup({});
  public teethList: ToothModel[] = [];
  public jaw = Jaw;
  public quadrant = Quadrant;
  public teethFormArray: UntypedFormArray;

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly router: Router,
    private readonly alertService: AlertService,
    private readonly procedureApiService: ProcedureApiService,
    private readonly toothApiService: ToothApiService,
  ) {
    this.teethFormArray = new UntypedFormArray([]);
  }

  ngOnInit() {
    this.loadValues();
  }

  public save = () => {
    this.saving = true;
    if (this.id) {
      this.update();
    } else {
      this.create();
    }
  }

  public close = () => {
    this.router.navigate(['admin/procedimientos']);
  }

  private loadValues = () => {
    this.activatedRoute.params.pipe(
      switchMap(params => {
        this.id = params.id;
        const procedure$ = this.id ? this.procedureApiService.getById(this.id, params.active) : of(new ProcedureApiModel());
        return forkJoin([procedure$, this.toothApiService.getAll()]);
      })
    ).subscribe({
      next: ([procedure, teethList]) => {
        teethList.sort((a, b) => a.number - b.number);
        const teethForQuadrant = 8;
        const totalTeeth = teethList.length;
        let teethUpper: ToothApiModel[] = [];
        let teethLower: ToothApiModel[] = [];
        for (let i = 0; i < teethForQuadrant; i++) {
          const firstQuadrant = teethList[i];
          const secondQuadrant = teethList[i + teethForQuadrant];
          const fourthQuadrant = teethList[(totalTeeth - 1) - i];
          const thirdQuadrant = teethList[totalTeeth - teethForQuadrant - 1 - i];
          teethUpper = teethUpper.concat(firstQuadrant, secondQuadrant);
          teethLower = teethLower.concat(fourthQuadrant, thirdQuadrant);
        }
        this.teethList = Object.assign([], teethUpper.concat(teethLower));
        this.teethList.forEach(item => {
          this.teethFormArray.push(
            new FormGroup({
              id: new FormControl(item.id),
              name: new FormControl(item.name),
              number: new FormControl(item.number),
              value: new FormControl(this.id ? procedure.procedureTeeth.find(x => x === item.id) : false),
            })
          );
        });
        this.teethFormArray.addValidators(this.minimumOneSelectedValidator);
        this.formGroup = new FormGroup( {
          name : new FormControl(this.id ? procedure.name : '', [Validators.required, Validators.maxLength(30)]),
          description : new FormControl(this.id ? procedure.description: '', [Validators.required, Validators.maxLength(50)]),
          estimatedSessions : new FormControl(this.id ? procedure.estimatedSessions: '', [Validators.required, Validators.maxLength(50)]),
          active : new FormControl(this.id ? procedure.active : true, [Validators.required]),
          teeth: this.teethFormArray
        });
        if (!this.id) {
          this.formGroup.controls.active.disable();
          this.title = 'actualizar';
        } else {
          this.formGroup.controls.estimatedSessions.disable();
          this.formGroup.controls.name.disable();
        }
        this.load = true;
      }, error: (e) => {
        this.load = true;
        throw new Error(e);
      }
    });
  }

  private update = (): void => {
    const request =  this.formGroup.getRawValue() as UpdateProcedureRequest;
    request.id = this.id;
    request.procedureTeeth = (this.teethFormArray.controls as FormGroup[]).filter((x: FormGroup) => x.get('value')?.value).map(x => x.get('id')?.value as string);
    this.procedureApiService.update(request).subscribe({
      next: () => this.saved(),
      error: (e) => {
        this.saving = false;
        throw new Error(e);
      }
    });
  }

  private create = () => {
    const request =  this.formGroup.getRawValue() as CreateProcedureRequest;
    request.procedureTeeth = (this.teethFormArray.controls as FormGroup[]).filter((x: FormGroup) => x.get('value')?.value).map(x => x.get('id')?.value as string);
    this.procedureApiService.create(request).subscribe({
      next: () => this.saved(),
      error: (e) => {
        this.saving = false;
        throw new Error(e);
      }
    });
  }

  private saved = (): void => {
    this.alertService.showSuccess('Procedimiento guardado.');
    this.close();
  }
  
  private minimumOneSelectedValidator = (abstractControl: AbstractControl): ValidationErrors | null => {
    const tooth = abstractControl as UntypedFormArray;
    const teethValues = tooth.controls.map(x => (x as FormGroup).get('value')?.value as boolean);
    return teethValues.some(x => x) ? null : { noneSelected : true };
  }
}
