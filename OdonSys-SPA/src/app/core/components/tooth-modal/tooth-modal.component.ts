import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngrx/store';
import { ProcedureFormGroup } from '../../forms/procedure-form-group.form';
import { ToothFormGroup } from '../../forms/tooth-form-group.form';
import { ToothApiModel } from '../../models/tooth/tooth-api-model';
import { ToothModel } from '../../models/tooth/tooth-model';
import { selectTeeth } from '../../../core/store/teeth/tooth.selectors';
import { ProcedureToothFormGroup } from '../../forms/procedure-tooth-form-group.form';
import { DifficultyProcedure } from '../../enums/difficulty-procedure.enum';
import { Jaw } from '../../enums/jaw.enum';
import { Quadrant } from '../../enums/quadrant.enum';
import { ToothModalModel } from '../../models/view/tooth-modal-model';
import { ProcedureToothModalModel } from '../../models/view/procedure-tooth-modal-model';
import { EnumHandler } from '../../helpers/enum-handler';

@Component({
  selector: 'app-tooth-modal',
  templateUrl: './tooth-modal.component.html',
  styleUrls: ['./tooth-modal.component.scss']
})
export class ToothModalComponent implements OnInit {
  @Input() procedure: FormGroup<ProcedureFormGroup>
  protected teethFormArray: FormArray<FormGroup<ToothFormGroup>> = new FormArray<FormGroup<ToothFormGroup>>([])
  protected difficultyOfProcedure = DifficultyProcedure
  protected jaw = Jaw
  protected quadrant = Quadrant
  public formGroup = new FormGroup({
    teeth: this.teethFormArray,
    color: new FormControl(this.difficultyOfProcedure.Rutinario),
    difficult: new FormControl({ value: this.difficultyOfProcedure.Rutinario, disabled: true}),
  })
  protected teethList: ToothModel[] = []

  constructor(
    public activeModal: NgbActiveModal,
    private store: Store,
  ) { }

  ngOnInit() {
    this.formGroup.controls.color.valueChanges.subscribe({
      next: (color) => {
        this.formGroup.controls.difficult.setValue(color)
      }
    })
    this.store.select(selectTeeth).subscribe({
      next: (teeth) => {
        teeth.sort((a, b) => a.number - b.number)
        const teethForQuadrant = 8
        const totalTeeth = teeth.length
        let teethUpper: ToothApiModel[] = []
        let teethLower: ToothApiModel[] = []
        for (let i = 0; i < teethForQuadrant; i++) {
          const firstQuadrant = teeth[i]
          const secondQuadrant = teeth[i + teethForQuadrant]
          const fourthQuadrant = teeth[(totalTeeth - 1) - i]
          const thirdQuadrant = teeth[totalTeeth - teethForQuadrant - 1 - i]
          teethUpper = teethUpper.concat(firstQuadrant, secondQuadrant)
          teethLower = teethLower.concat(fourthQuadrant, thirdQuadrant)
        }
        this.teethList = teethUpper.concat(teethLower)
        this.teethList.forEach(item => {
          const toothSelected = !!this.procedure.controls.toothIds?.controls.find(x => x.value.id === item.id)
          this.teethFormArray.push(
            new FormGroup<ToothFormGroup>({
              id: new FormControl(item.id),
              name: new FormControl(item.name),
              number: new FormControl(item.number),
              value: new FormControl(toothSelected),
              jaw: new FormControl(item.jaw),
              quadrant: new FormControl(item.quadrant),
            })
          )
        })
        this.formGroup.controls.color.setValue(this.procedure.controls.color?.value! as DifficultyProcedure)
        this.formGroup.controls.color.updateValueAndValidity()
      }, error: (e) => {
        throw e
      }
    })
  }

  protected save = () => {
    const teeth: ToothModalModel[] = []
    this.teethFormArray.controls.filter(x => x.value.value).forEach(x => teeth.push(new ToothModalModel(x.value.id!, x.value.number!)))
    const model = new ProcedureToothModalModel(this.formGroup.value.color!, teeth)
    this.activeModal.close(model)
  }

}
