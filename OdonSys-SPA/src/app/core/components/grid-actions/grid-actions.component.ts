import { Component } from '@angular/core';
import { AgRendererComponent } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';
import { ButtonGridActionType } from '../../enums/button-grid-action-type.enum';
import { ConditionalGridButtonShow } from '../../models/view/conditional-grid-button-show';
import { GridActionModel } from '../../models/view/grid-action-model';

@Component({
  selector: 'app-grid-actions',
  templateUrl: './grid-actions.component.html',
  styleUrls: ['./grid-actions.component.scss']
})
export class GridActionsComponent implements AgRendererComponent {
  public params!: GridActionModel;
  public canApprove = false;
  public canShowView = false;
  public canShowEdit = false;
  public canShowDelete = false;
  public canShowDeactivate = false;
  public canShowDownload = false;

  constructor() {
  }

  public agInit = (params: ICellRendererParams & GridActionModel): void => {
    this.params = params;
    console.log(params.data);
    if(this.params.conditionalButtons && this.params.conditionalButtons.length > 0) {
      this.params.conditionalButtons.forEach((x: ConditionalGridButtonShow) => {
        const attributeValue = params.data[x.attributeAffected];
        if (attributeValue === x.attributeValue) {
        }
      });
    } else {
      this.canApprove = this.params.buttonShow.includes(ButtonGridActionType.Aprobar);
      this.canShowView = this.params.buttonShow.includes(ButtonGridActionType.Ver);
      this.canShowEdit = this.params.buttonShow.includes(ButtonGridActionType.Editar);
      this.canShowDelete = this.params.buttonShow.includes(ButtonGridActionType.Borrar);
      this.canShowDeactivate = this.params.buttonShow.includes(ButtonGridActionType.Desactivar);
      this.canShowDownload = this.params.buttonShow.includes(ButtonGridActionType.Descargar);
    }
  }

  public approveItem = () => this.params.clicked(ButtonGridActionType.Aprobar);

  public viewItem = () => this.params.clicked(ButtonGridActionType.Ver);

  public editItem = () => this.params.clicked(ButtonGridActionType.Editar);

  public deleteItem = () => this.params.clicked(ButtonGridActionType.Borrar);

  public deactivateItem = () => this.params.clicked(ButtonGridActionType.Desactivar);

  public downloadItem = () => this.params.clicked(ButtonGridActionType.Descargar);
  
  public refresh = (params: any): boolean => {
    return true;
  }
}


