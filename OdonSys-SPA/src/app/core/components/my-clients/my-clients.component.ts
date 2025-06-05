import { Component, HostListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ColDef, GridApi, GridOptions, GridReadyEvent } from 'ag-grid-community';
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

  protected loading = false
  protected ready = false
  protected gridOptions!: GridOptions
  protected canCreate = false
  private attributeActive!: string
  private canEdit = false
  private canShowReport = false
  private gridApi!: GridApi
  private canShowMyOrthodontic = false

  constructor(
    private readonly clientApiService: ClientApiService,
    private readonly agGridService: AgGridService,
    private readonly router: Router,
    private userInfoService: UserInfoService
  ) { }

  ngOnInit() {
    // eslint-disable-next-line @typescript-eslint/no-non-null-asserted-optional-chain
    this.attributeActive = (environment.systemAttributeModel as SystemAttributeModel[]).find(x => x.id === FieldId.Active)?.value!
    this.canCreate = this.userInfoService.havePermission(Permission.CreateClients)
    this.canEdit = this.userInfoService.havePermission(Permission.UpdateClients)
    this.canShowReport = this.userInfoService.havePermission(Permission.AccessMyInvoices)
    this.canShowMyOrthodontic = this.userInfoService.havePermission(Permission.AccessOrthodontics)
    this.setupAgGrid()
    this.ready = true
  }

  protected prepareGrid = (event: GridReadyEvent<any, any>) => {
    this.gridApi = event.api
    this.getList()
  }

  private getList = (): void => {
    this.loading = true;
    this.clientApiService.getDoctorPatients().subscribe({
      next: (response: ClientApiModel[]) => {
        // this.gridOptions.rowData = response
        this.gridApi?.sizeColumnsToFit()
        this.gridApi?.setGridOption('rowData', response)
        if (response.length === 0) {
          this.gridApi?.showNoRowsOverlay()
        }
        this.loading = false
      }, error: (e) => {
        this.gridApi?.showNoRowsOverlay()
        this.loading = false
        throw e
      }
    })
  }

  @HostListener('window:resize', ['$event'])
  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  private getScreenSize(event?: any) {
    this.gridApi?.sizeColumnsToFit()
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getClientGridOptions()
    const columnAction = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action') as ColDef
    const buttonsToShow: ButtonGridActionType[] = []
    if (this.canShowReport) {
      buttonsToShow.push(ButtonGridActionType.Ver)
    }
    if (this.canEdit) {
      buttonsToShow.push(ButtonGridActionType.Editar)
    }
    if (this.canShowMyOrthodontic) {
      buttonsToShow.push(ButtonGridActionType.Ortodoncias)
    }
    const params: GridActionModel = {
      buttonShow: buttonsToShow,
      clicked: this.actionColumnClicked,
      customButton:  this.canShowReport ? new CustomGridButtonShow(' Reporte', 'fa-file-lines') : undefined
    }
    columnAction.cellRendererParams = params
  }

  private actionColumnClicked = (action: ButtonGridActionType): void => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridApi)
    const url = this.router.url
    const id = currentRowNode.data.id
    switch (action) {
      case ButtonGridActionType.Ver:
        this.router.navigate([`${url}/ver/${id}`])
        break
      case ButtonGridActionType.Editar:
        this.router.navigate([`${url}/actualizar/${id}`])
        break
      case ButtonGridActionType.CustomButton:
        this.router.navigate([`${url}/reporte/${id}`])
        break
      case ButtonGridActionType.Ortodoncias:
        this.router.navigate([`${url}/mis-ortodoncias/${id}`])
        break
      default:
        break
    }
  }

}
