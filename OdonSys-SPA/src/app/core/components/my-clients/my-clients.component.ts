import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ColDef, GridOptions } from 'ag-grid-community';
import { environment } from '../../../../environments/environment';
import { ButtonGridActionType } from '../../enums/button-grid-action-type.enum';
import { FieldId } from '../../enums/field-id.enum';
import { Permission } from '../../enums/permission.enum';
import { ClientApiModel } from '../../models/api/clients/client-api-model';
import { GridActionModel } from '../../models/view/grid-action-model';
import { SystemAttributeModel } from '../../models/view/system-attribute-model';
import { ClientApiService } from '../../services/api/client-api.service';
import { AgGridService } from '../../services/shared/ag-grid.service';
import { AlertService } from '../../services/shared/alert.service';
import { UserInfoService } from '../../services/shared/user-info.service';

@Component({
  selector: 'app-clients',
  templateUrl: './my-clients.component.html',
  styleUrls: ['./my-clients.component.scss']
})
export class ClientsComponent implements OnInit {
  public loading: boolean = false
  public ready: boolean = false
  public gridOptions!: GridOptions
  private attributeActive!: string
  protected canCreate: boolean = false

  constructor(
    private readonly clientApiService: ClientApiService,
    private readonly agGridService: AgGridService,
    private readonly router: Router,
    private readonly alertService: AlertService,
    private userInfoService: UserInfoService,
  ) { }

  ngOnInit() {
    this.attributeActive = (environment.systemAttributeModel as SystemAttributeModel[]).find(x => x.id === FieldId.Active)?.value!
    this.canCreate = this.userInfoService.havePermission(Permission.CreateClients)
    this.setupAgGrid()
    this.ready = true
    this.getList()
  }

  private getList = () => {
    this.loading = true;
    this.clientApiService.getDoctorPatients().subscribe({
      next: (response: ClientApiModel[]) => {
        this.gridOptions.api?.setRowData(response)
        this.gridOptions.api?.sizeColumnsToFit()
        if (response.length === 0) {
          this.gridOptions.api?.showNoRowsOverlay()
        }
        this.loading = false
      }, error: (e) => {
        this.gridOptions.api?.showNoRowsOverlay()
        this.loading = false
        throw e
      }
    })
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getClientGridOptions()
    const columnAction = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action') as ColDef
    const params: GridActionModel = {
      buttonShow: [ButtonGridActionType.Editar, ButtonGridActionType.Ver],
      clicked: this.actionColumnClicked
    }
    columnAction.cellRendererParams = params
  }

  private actionColumnClicked = (action: ButtonGridActionType): void => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions)
    switch (action) {
      case ButtonGridActionType.Ver:
        this.alertService.showInfo('No implementado.')
        // this.router.navigate([`${this.router.url}/ver/${currentRowNode.data.id}`])
        break
      case ButtonGridActionType.Editar:
        this.router.navigate([`${this.router.url}/actualizar/${currentRowNode.data.id}`])
        break
      default:
        break
    }
  }

}
