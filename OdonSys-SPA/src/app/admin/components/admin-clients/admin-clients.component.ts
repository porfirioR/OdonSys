import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ColDef, GridOptions } from 'ag-grid-community';
import { ClientApiModel } from '../../../core/models/api/clients/client-api-model';
import { AgGridService } from '../../../core/services/shared/ag-grid.service';
import { ButtonGridActionType } from '../../../core/enums/button-grid-action-type.enum';
import { GridActionModel } from '../../../core/models/view/grid-action-model';
import { AlertService } from '../../../core/services/shared/alert.service';
import { ClientAdminApiService } from '../../service/client-admin-api.service';
import { PatchRequest } from '../../../core/models/api/patch-request';
import { ConditionalGridButtonShow } from '../../../core/models/view/conditional-grid-button-show';
import { SystemAttributeModel } from '../../../core/models/view/system-attribute-model';
import { environment } from '../../../../environments/environment';
import { FieldId } from '../../../core/enums/field-id.enum';

@Component({
  selector: 'app-admin-clients',
  templateUrl: './admin-clients.component.html',
  styleUrls: ['./admin-clients.component.scss']
})
export class AdminClientsComponent implements OnInit {
  public loading: boolean = false;
  public ready: boolean = false;
  public gridOptions!: GridOptions;
  private attributeActive!: string;

  constructor(
    private readonly alertService: AlertService,
    private readonly clientAdminApiService: ClientAdminApiService,
    private readonly agGridService: AgGridService,
    private readonly router: Router
  ) { }

  ngOnInit() {
    this.attributeActive = (environment.systemAttributeModel as SystemAttributeModel[]).find(x => x.id === FieldId.Active)?.value!;
    this.setupAgGrid();
    this.ready = true;
    this.getList();
  }

  private getList = () => {
    this.loading = true;
    this.clientAdminApiService.getAll().subscribe({
      next: (response: ClientApiModel[]) => {
        this.gridOptions.api?.setRowData(response);
        this.gridOptions.api?.sizeColumnsToFit();
        if (response.length === 0) {
          this.gridOptions.api?.showNoRowsOverlay();
        }
        this.loading = false;
      }, error: (e) => {
        this.gridOptions.api?.showNoRowsOverlay();
        this.loading = false;
        throw e;
      }
    });
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getAdminClientGridOptions();
    const columnAction = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action') as ColDef;
    columnAction.minWidth = 300;
    const params: GridActionModel = {
      buttonShow: [ButtonGridActionType.Editar, ButtonGridActionType.Ver],
      clicked: this.actionColumnClicked,
      conditionalButtons: [
        new ConditionalGridButtonShow(this.attributeActive, true.toString(), ButtonGridActionType.Desactivar),
        new ConditionalGridButtonShow(this.attributeActive, false.toString(), ButtonGridActionType.Aprobar)
      ]
    };
    columnAction.cellRendererParams = params;
  }

  private actionColumnClicked = (action: ButtonGridActionType): void => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions);
    switch (action) {
      case ButtonGridActionType.Aprobar:
      case ButtonGridActionType.Desactivar:
        this.changeSelectedClientVisibility(currentRowNode.data);
        break;
      case ButtonGridActionType.Ver:
        this.router.navigate([`${this.router.url}/ver/${currentRowNode.data.id}`]);
        break;
      case ButtonGridActionType.Editar:
        this.router.navigate([`${this.router.url}/actualizar/${currentRowNode.data.id}`]);
        break;
      default:
        break;
    }
  }

  private changeSelectedClientVisibility = (client: ClientApiModel): void => {
    const message = client.active ?
                    '¿Está seguro de deshabilitar al paciente, no será visible para los doctores?' :
                    '¿Está seguro de habilitar al paciente, será visible para los doctores?';
    this.alertService.showQuestionModal(message).then((result) => {
      if (result.value) {
        this.loading = true;
        const request = new PatchRequest(!client.active);
        this.clientAdminApiService.clientVisibility(client.id, request).subscribe({
          next: (response) => {
            this.loading = false;
            client.active = response.active;
            this.alertService.showSuccess('Visibilidad del paciente ha sido actualizado.');
            this.getList();
          }, error: (e) => {
            this.loading = false;
            throw e;
          }
        });
      }
    });
  }

}
