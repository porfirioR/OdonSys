import { Component, OnInit } from '@angular/core';
import { ICellRendererParams } from 'ag-grid-community';
import { ButtonGridActionType } from '../../enums/button-grid-action-type.enum';
import { GridActionModel } from '../../models/view/grid-action-model';

@Component({
  selector: 'app-grid-actions',
  templateUrl: './grid-actions.component.html',
  styleUrls: ['./grid-actions.component.scss']
})
export class GridActionsComponent implements OnInit {
  public params: GridActionModel = {
    buttonShow: [],
    clicked: function (action: ButtonGridActionType): void {
      throw new Error('Function not implemented.');
    }
  };
  public canShowView = false;
  public canShowEdit = false;
  public canShowDelete = false;
  public canShowDownload = false;
  public canShowPost = false;
  public canShowUnlock = false;
  public canSearchRecent = false;
  public canShowViewDetails = false;

  constructor() {
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  public agInit = (params: ICellRendererParams): void => {
    this.params =  params as unknown as GridActionModel;
    this.canShowView = this.params.buttonShow.includes(ButtonGridActionType.Ver);
    this.canShowEdit = this.params.buttonShow.includes(ButtonGridActionType.Editar);
    this.canShowDelete = this.params.buttonShow.includes(ButtonGridActionType.Borrar);
    this.canShowDownload = this.params.buttonShow.includes(ButtonGridActionType.Descargar);
  }

  public viewItem = () => this.params.clicked(ButtonGridActionType.Ver);

  public editItem = () => this.params.clicked(ButtonGridActionType.Editar);

  public deleteItem = () => this.params.clicked(ButtonGridActionType.Borrar);

  public downloadItem = () => this.params.clicked(ButtonGridActionType.Descargar);
  
  public refresh = (params: any): boolean => {
    return true;
  }
}


