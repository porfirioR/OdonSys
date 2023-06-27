import { Component } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';
import { GridBadgeModel } from '../../models/view/grid-badge-model';

@Component({
  selector: 'app-grid-badge',
  templateUrl: './grid-badge.component.html',
  styleUrls: ['./grid-badge.component.scss']
})
export class GridBadgeComponent implements ICellRendererAngularComp {
  protected badge: string = 'text-bg-primary'
  protected title: string = ''

  constructor() { }

  agInit(params: ICellRendererParams & GridBadgeModel[] & any): void {
    this.configureCellRenderComponent(params)
  }

  refresh(params: ICellRendererParams<any, any, any>): boolean {
    this.configureCellRenderComponent(params)
    return true
  }

  private configureCellRenderComponent = (params: ICellRendererParams & GridBadgeModel[] & any) => {
    const gridBadgeModelList: GridBadgeModel[] = params.badgeParams as GridBadgeModel[]
    const gridBadge = gridBadgeModelList.find(x => x.type === params.data[params.colDef.field])
    this.badge = `text-bg-${gridBadge!.badge}`
    this.title = gridBadge!.title
  }
}
