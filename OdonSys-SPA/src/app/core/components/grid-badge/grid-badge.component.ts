import { Component, OnInit } from '@angular/core';
import { AgRendererComponent } from 'ag-grid-angular';
import { ICellRendererParams } from 'ag-grid-community';
import { InvoiceStatus } from '../../enums/invoice-status.enum';
import { GridBadgeModel } from '../../models/view/grid-badge-model';

@Component({
  selector: 'app-grid-badge',
  templateUrl: './grid-badge.component.html',
  styleUrls: ['./grid-badge.component.scss']
})
export class GridBadgeComponent implements AgRendererComponent {
  protected badge: string = 'text-bg-primary'
  protected title: string = ''

  constructor() { }

  agInit(params: ICellRendererParams & GridBadgeModel[] & any): void {
    const gridBadgeModelList: GridBadgeModel[] = params.badgeParams as GridBadgeModel[]
    const gridBadge = gridBadgeModelList.find(x => x.type === params.data[params.colDef.field])
    this.badge = `text-bg-${gridBadge!.badge}`
    this.title = gridBadge!.title
  }

  refresh(params: ICellRendererParams<any, any, any>): boolean {
    throw new Error('Method not implemented.');
  }

  ngOnInit() {
  }

}
