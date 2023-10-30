import { Component } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';
import { ButtonGridActionType } from '../../enums/button-grid-action-type.enum';
import { OperationType } from '../../enums/operation-type.enum';
import { ConditionalGridButtonShow } from '../../models/view/conditional-grid-button-show';
import { GridActionModel } from '../../models/view/grid-action-model';
import { ColorType } from '../../constants/color-type';

@Component({
  selector: 'app-grid-actions',
  templateUrl: './grid-actions.component.html',
  styleUrls: ['./grid-actions.component.scss']
})
export class GridActionsComponent implements ICellRendererAngularComp {
  protected params!: GridActionModel
  protected canApprove = false
  protected canShowView = false
  protected canRestore = false
  protected canShowEdit = false
  protected canShowDelete = false
  protected canShowDeactivate = false
  protected canShowDownload = false
  protected canShowCustomButton = false
  protected customColorButton: string = 'btn-outline-'
  private customColor: ColorType = 'info'

  public agInit(params: ICellRendererParams<any, any, any> & GridActionModel): void {
    this.configureCellRenderComponent(params)
  }

  protected approveItem = () => this.params.clicked(ButtonGridActionType.Aprobar)

  protected viewItem = () => this.params.clicked(ButtonGridActionType.Ver)

  protected editItem = () => this.params.clicked(ButtonGridActionType.Editar)

  protected deleteItem = () => this.params.clicked(ButtonGridActionType.Borrar)

  protected deactivateItem = () => this.params.clicked(ButtonGridActionType.Desactivar)

  protected downloadItem = () => this.params.clicked(ButtonGridActionType.Descargar)

  protected restoreItem = () => this.params.clicked(ButtonGridActionType.Restaurar)

  protected customItem = () => this.params.clicked(ButtonGridActionType.CustomButton)

  public refresh(params: ICellRendererParams<any, any, any> & GridActionModel): boolean {
    this.resetAllValues()
    this.configureCellRenderComponent(params)
    return true
  }

  private basicVerifyAttributes = (params: ICellRendererParams, row: ConditionalGridButtonShow) => {
    const attributeValue = params.data[row.attributeAffected]
    if(row.operator === OperationType.Equal && attributeValue.toString() === row.attributeValue) {
      this.verifyButtons(row.buttonType)
    } else if(row.operator === OperationType.NotEqual && attributeValue.toString() !== row.attributeValue) {
      this.verifyButtons(row.buttonType)
    }
  }

  private verifyButtons = (action: ButtonGridActionType) => {
    switch (action) {
      case ButtonGridActionType.Aprobar:
        this.canApprove = true
        break
      case ButtonGridActionType.Editar:
        this.canShowEdit = true
        break
      case ButtonGridActionType.Ver:
        this.canShowView = true
        break
      case ButtonGridActionType.Restaurar:
        this.canRestore = true
        break
      case ButtonGridActionType.Borrar:
        this.canShowDelete = true
        break
      case ButtonGridActionType.Descargar:
        this.canShowDownload = true
        break
      case ButtonGridActionType.Desactivar:
        this.canShowDeactivate = true
        break
      case ButtonGridActionType.CustomButton:
        this.canShowCustomButton = true
        break
      default:
        break
    }
  }

  private configureCellRenderComponent = (params: ICellRendererParams & GridActionModel) => {
    this.params = params
    if(this.params.conditionalButtons && this.params.conditionalButtons.length > 0) {
      this.params.conditionalButtons.forEach((x: ConditionalGridButtonShow) => {
        if (x.principalAttributeAffected) {
          const principalAttributeValue = params.data[x.principalAttributeAffected]
          if(x.principalOperator === OperationType.Equal && principalAttributeValue.toString() === x.principalAttributeValue) {
            this.basicVerifyAttributes(params, x)
          } else if(x.principalOperator === OperationType.NotEqual && principalAttributeValue.toString() !== x.principalAttributeValue) {
            this.basicVerifyAttributes(params, x)
          }
        } else {
          this.basicVerifyAttributes(params, x)
        }
      })
    }
    if (this.params.customButton) {
      if (!this.params.customButton.isConditionalButton) {
        this.verifyButtons(ButtonGridActionType.CustomButton)
      }
      this.customColor = this.params.customButton.customColor
    }
    this.customColorButton += this.customColor
    this.params.buttonShow.forEach(x => this.verifyButtons(x))
  }

  private resetAllValues = () => {
    this.canApprove = this.canShowView = this.canRestore = this.canShowEdit = this.canShowDelete =
    this.canShowDeactivate = this.canShowDownload = this.canShowCustomButton = false
    this.customColor = 'info'
    this.customColorButton = 'btn-outline-'
  }

}
