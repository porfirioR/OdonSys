import { Component, HostListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ColDef, GridOptions } from 'ag-grid-community';
import { environment } from '../../../../environments/environment';
import { ButtonGridActionType } from '../../enums/button-grid-action-type.enum';
import { FieldId } from '../../enums/field-id.enum';
import { Permission } from '../../enums/permission.enum';
import { ClientApiModel } from '../../models/api/clients/client-api-model';
import { GridActionModel } from '../../models/view/grid-action-model';
import { SystemAttributeModel } from '../../models/view/system-attribute-model';
import { CustomGridButtonShow } from '../../models/view/custom-grid-button-show';
import { ClientApiService } from '../../services/api/client-api.service';
import { AgGridService } from '../../services/shared/ag-grid.service';
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
  private canEdit = false
  protected canCreate: boolean = false

  constructor(
    private readonly clientApiService: ClientApiService,
    private readonly agGridService: AgGridService,
    private readonly router: Router,
    private userInfoService: UserInfoService,
  ) { }

  ngOnInit() {
    this.attributeActive = (environment.systemAttributeModel as SystemAttributeModel[]).find(x => x.id === FieldId.Active)?.value!
    this.canCreate = this.userInfoService.havePermission(Permission.CreateClients)
    this.canEdit = this.userInfoService.havePermission(Permission.UpdateClients)
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

  @HostListener('window:resize', ['$event'])
  private getScreenSize(event?: any) {
    this.gridOptions.api?.sizeColumnsToFit()
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getClientGridOptions()
    const columnAction = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action') as ColDef
    const buttonsToShow: ButtonGridActionType[] = [
      ButtonGridActionType.Ver
    ]
    if (this.canEdit) {
      buttonsToShow.push(ButtonGridActionType.Editar)
    }
    const params: GridActionModel = {
      buttonShow: buttonsToShow,
      clicked: this.actionColumnClicked,
      customButton:  new CustomGridButtonShow(' Reporte', 'fa-file-lines')
    }
    columnAction.cellRendererParams = params
  }

  private actionColumnClicked = (action: ButtonGridActionType): void => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions)
    switch (action) {
      case ButtonGridActionType.Ver:
        this.router.navigate([`${this.router.url}/ver/${currentRowNode.data.id}`])
        break
      case ButtonGridActionType.Editar:
        this.router.navigate([`${this.router.url}/actualizar/${currentRowNode.data.id}`])
        break
      case ButtonGridActionType.CustomButton:
        this.router.navigate([`${this.router.url}/reporte/${currentRowNode.data.id}`])
        break
      default:
        break
    }
  }

}
