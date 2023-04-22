import { Component, OnInit } from '@angular/core';
import { UntypedFormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ToothModel } from '../../../../core/models/tooth/tooth-model';
import { Jaw } from '../../../../core/enums/jaw.enum';
import { Quadrant } from '../../../../core/enums/quadrant.enum';
import { CreateProcedureRequest } from '../../../../core/models/procedure/create-procedure-request';
import { UpdateProcedureRequest } from '../../../../core/models/procedure/update-procedure-request';
import { savingSelector } from '../../../../core/store/saving/saving.selector';
import { selectProcedures } from '../../../../core/store/procedure/procedure.selectors';
import * as fromProceduresActions from '../../../../core/store/procedure/procedure.actions';

@Component({
  selector: 'app-upsert-procedure',
  templateUrl: './upsert-procedure.component.html',
  styleUrls: ['./upsert-procedure.component.scss']
})
export class UpsertProcedureComponent implements OnInit {
  public load: boolean = false;
  public saving$: Observable<boolean> = this.store.select(savingSelector)
  public title = 'crear';
  private id = '';
  public formGroup = new FormGroup( {
    name : new FormControl('', [Validators.required, Validators.maxLength(60)]),
    description : new FormControl('', [Validators.required, Validators.maxLength(100)]),
    active : new FormControl(false, [Validators.required]),
    price : new FormControl(0, [Validators.required, Validators.min(0)]),
  })
  public teethList: ToothModel[] = []
  public jaw = Jaw
  public quadrant = Quadrant
  public teethFormArray: UntypedFormArray = new UntypedFormArray([])

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly router: Router,
    private store: Store,
  ) { }

  ngOnInit() {
    this.loadValues();
  }

  protected save = () => {
    this.id ? this.update() : this.create()
  }

  protected close = () => {
    this.router.navigate(['admin/procedimientos'])
  }

  private loadValues = () => {
    this.id = this.activatedRoute.snapshot.params['id']
    const isUpdateUrl = this.activatedRoute.snapshot.url[1].path === 'actualizar'
    const procedure$ = this.store.pipe(
      select(selectProcedures),
      map(x => this.id ? x.find(y => y.id === this.id) ?? undefined : undefined)
    )
    procedure$.subscribe({
      next: (data) => {
        if (isUpdateUrl && !data) {
          this.router.navigate(['admin/procedimientos/crear'])
        }
        if (data) {
          this.title = 'Actualizar'
          this.formGroup.controls.name!.setValue(data.name)
          this.formGroup.controls.description.setValue(data.description)
          this.formGroup.controls.price.setValue(data.price)
        }
        this.load = true
      }, error: (e) => {
        this.load = true
        throw e
      }
    })
    // this.activatedRoute.params.pipe(
    //   switchMap(params => {
    //     this.id = params.id;
    //     const procedure$ = this.procedureApiService.getById(this.id, params.active)
    //     return forkJoin([procedure$, this.toothApiService.getAll()]);
    //   })
    // ).subscribe({
    //   next: ([procedure, teethList]) => {
    //     teethList.sort((a, b) => a.number - b.number);
    //     const teethForQuadrant = 8;
    //     const totalTeeth = teethList.length;
    //     let teethUpper: ToothApiModel[] = [];
    //     let teethLower: ToothApiModel[] = [];
    //     for (let i = 0; i < teethForQuadrant; i++) {
    //       const firstQuadrant = teethList[i];
    //       const secondQuadrant = teethList[i + teethForQuadrant];
    //       const fourthQuadrant = teethList[(totalTeeth - 1) - i];
    //       const thirdQuadrant = teethList[totalTeeth - teethForQuadrant - 1 - i];
    //       teethUpper = teethUpper.concat(firstQuadrant, secondQuadrant);
    //       teethLower = teethLower.concat(fourthQuadrant, thirdQuadrant);
    //     }
    //     this.teethList = teethUpper.concat(teethLower);
    //     this.teethList.forEach(item => {
    //       this.teethFormArray.push(
    //         new FormGroup({
    //           id: new FormControl(item.id),
    //           name: new FormControl(item.name),
    //           number: new FormControl(item.number),
    //           value: new FormControl(this.id ? procedure.procedureTeeth.find(x => x === item.id) : false),
    //         })
    //       );
    //     });
    //     this.teethFormArray.addValidators(this.minimumOneSelectedValidator);
    //     if (this.id) {
    //       this.formGroup.controls.name.setValue(procedure.name)
    //       this.formGroup.controls.description.setValue(procedure.description)
    //       this.formGroup.controls.active.setValue(procedure.active)

    //       this.formGroup.controls.active.disable()
    //       this.title = 'actualizar';
    //       this.formGroup.controls.name.disable();
    //     }
    //     this.load = true;
    //   }, error: (e) => {
    //     this.load = true;
    //     throw e;
    //   }
    // });
  }

  private update = (): void => {
    const request =  new UpdateProcedureRequest(
      this.id,
      this.formGroup.value.description!,
      this.formGroup.value.price!,
      (this.teethFormArray.controls as FormGroup[]).filter((x: FormGroup) => x.get('value')?.value).map(x => x.get('id')?.value as string)
    )
    this.store.dispatch(fromProceduresActions.updateProcedure({ procedure: request }))
  }

  private create = () => {
    const request =  new CreateProcedureRequest(
      this.formGroup.value.name!,
      this.formGroup.value.description!,
      this.formGroup.value.price!,
      (this.teethFormArray.controls as FormGroup[]).filter((x: FormGroup) => x.get('value')?.value).map(x => x.get('id')?.value as string)
    )
    this.store.dispatch(fromProceduresActions.addProcedure({ procedure: request }))
  }

  // private minimumOneSelectedValidator = (abstractControl: AbstractControl): ValidationErrors | null => {
  //   const tooth = abstractControl as UntypedFormArray;
  //   const teethValues = tooth.controls.map(x => (x as FormGroup).get('value')?.value as boolean);
  //   return teethValues.some(x => x) ? null : { noneSelected : true };
  // }
}
