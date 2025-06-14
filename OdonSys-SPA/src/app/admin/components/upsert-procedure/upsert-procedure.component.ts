import { Component, OnInit } from '@angular/core';
import { UntypedFormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Jaw } from '../../../core/enums/jaw.enum';
import { Quadrant } from '../../../core/enums/quadrant.enum';
import { Permission } from '../../../core/enums/permission.enum';
import { ToothModel } from '../../../core/models/tooth/tooth-model';
import { CreateProcedureRequest } from '../../../core/models/procedure/create-procedure-request';
import { UpdateProcedureRequest } from '../../../core/models/procedure/update-procedure-request';
import { savingSelector } from '../../../core/store/saving/saving.selector';
import { selectProcedures } from '../../../core/store/procedures/procedure.selectors';
import * as fromProceduresActions from '../../../core/store/procedures/procedure.actions';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { SubscriptionService } from '../../../core/services/shared/subscription.service';

@Component({
  selector: 'app-upsert-procedure',
  templateUrl: './upsert-procedure.component.html',
  styleUrls: ['./upsert-procedure.component.scss']
})
export class UpsertProcedureComponent implements OnInit {
  public formGroup = new FormGroup({
    name : new FormControl('', [Validators.required, Validators.maxLength(60)]),
    description : new FormControl('', [Validators.required, Validators.maxLength(100)]),
    active : new FormControl(true, [Validators.required]),
    xRays : new FormControl(false),
    price : new FormControl(1, [Validators.required, Validators.min(0)]),
  })
  public ignorePreventUnsavedChanges = false
  protected saving$: Observable<boolean> = of(false)
  protected title = 'Crear'
  protected canRestore = false
  protected teethList: ToothModel[] = []
  protected jaw = Jaw
  protected quadrant = Quadrant
  protected teethFormArray: UntypedFormArray = new UntypedFormArray([])
  private id = ''

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly router: Router,
    private store: Store,
    private userInfoService: UserInfoService,
    private readonly subscriptionService: SubscriptionService
  ) { }

  ngOnInit() {
    this.saving$ = this.store.select(savingSelector)
    this.subscriptionService.onErrorInSave.subscribe({ next: () => { this.ignorePreventUnsavedChanges = false } })
    this.loadValues()
  }

  protected save = () => {
    this.ignorePreventUnsavedChanges = true
    if (this.id) {
      this.update()
    } else {
      this.create()
    }
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
          this.formGroup.patchValue({
            name: data.name,
            description: data.description,
            price: data.price,
            active: data.active,
            xRays: data.xRays
          })
          this.canRestore = this.userInfoService.havePermission(Permission.DeleteProcedures) && !data.active
          this.formGroup.controls.name.disable()
        }
        this.formGroup.controls.name.markAsTouched()
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
      this.formGroup.controls.active.value!,
      (this.teethFormArray.controls as FormGroup[]).filter((x: FormGroup) => x.get('value')?.value).map(x => x.get('id')?.value as string),
      this.formGroup.value.xRays!
    )
    this.store.dispatch(fromProceduresActions.updateProcedure({ procedure: request }))
  }

  private create = () => {
    const request =  new CreateProcedureRequest(
      this.formGroup.value.name!,
      this.formGroup.value.description!,
      this.formGroup.value.price!,
      (this.teethFormArray.controls as FormGroup[]).filter((x: FormGroup) => x.get('value')?.value).map(x => x.get('id')?.value as string),
      this.formGroup.value.xRays!
    )
    this.store.dispatch(fromProceduresActions.addProcedure({ procedure: request }))
  }

  // private minimumOneSelectedValidator = (abstractControl: AbstractControl): ValidationErrors | null => {
  //   const tooth = abstractControl as UntypedFormArray;
  //   const teethValues = tooth.controls.map(x => (x as FormGroup).get('value')?.value as boolean);
  //   return teethValues.some(x => x) ? null : { noneSelected : true };
  // }
}
