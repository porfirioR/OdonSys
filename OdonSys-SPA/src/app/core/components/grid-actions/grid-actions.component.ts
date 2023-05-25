import { Component } from '@angular/core';
import { AgRendererComponent } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';
import { ButtonGridActionType } from '../../enums/button-grid-action-type.enum';
import { OperationType } from '../../enums/operation-type.enum';
import { ConditionalGridButtonShow } from '../../models/view/conditional-grid-button-show';
import { GridActionModel } from '../../models/view/grid-action-model';

@Component({
  selector: 'app-grid-actions',
  templateUrl: './grid-actions.component.html',
  styleUrls: ['./grid-actions.component.scss']
})
export class GridActionsComponent implements AgRendererComponent {
  public params!: GridActionModel
  public canApprove = false
  public canShowView = false
  public canRestore = false
  public canShowEdit = false
  public canShowDelete = false
  public canShowDeactivate = false
  public canShowDownload = false
  public canShowCustomButton = false

  constructor() { }

  public agInit = (params: ICellRendererParams & GridActionModel): void => {
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
    if (this.params.customButton && !this.params.customButton.isConditionalButton) {
      this.verifyButtons(ButtonGridActionType.CustomButton)
    }
    this.params.buttonShow.forEach(x => this.verifyButtons(x))
  }

  public approveItem = () => this.params.clicked(ButtonGridActionType.Aprobar)

  public viewItem = () => this.params.clicked(ButtonGridActionType.Ver)

  public editItem = () => this.params.clicked(ButtonGridActionType.Editar)

  public deleteItem = () => this.params.clicked(ButtonGridActionType.Borrar)

  public deactivateItem = () => this.params.clicked(ButtonGridActionType.Desactivar)

  public downloadItem = () => this.params.clicked(ButtonGridActionType.Descargar)

  public restoreItem = () => this.params.clicked(ButtonGridActionType.Restaurar)

  public customItem = () => this.params.clicked(ButtonGridActionType.CustomButton)

  public refresh = (params: any): boolean => {
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
}


