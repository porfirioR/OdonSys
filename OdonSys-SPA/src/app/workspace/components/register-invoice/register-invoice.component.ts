import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AbstractControl, FormArray, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, combineLatest, debounceTime, filter, forkJoin, of, switchMap, tap } from 'rxjs';

import { ClientModel } from '../../../core/models/view/client-model';
import { ProcedureModel } from '../../../core/models/procedure/procedure-model';
import { CreateClientRequest } from '../../../core/models/api/clients/create-client-request';
import { CreateClientProcedureRequest } from '../../models/client-procedures/create-client-procedure-request';
import { CreateInvoiceRequest } from '../../models/invoices/api/create-invoice-request';
import { CreateInvoiceDetailRequest } from '../../models/invoices/api/create-invoice-detail-request';
import { InvoiceApiModel } from '../../models/invoices/api/invoice-api-model';
import { AssignClientRequest } from '../../../core/models/api/clients/assign-client-request';

import  * as fromClientsActions from '../../../core/store/clients/client.actions';
import { selectActiveClients } from '../../../core/store/clients/client.selectors';

import  * as fromProceduresActions from '../../../core/store/procedures/procedure.actions';
import { selectActiveProcedures } from '../../../core/store/procedures/procedure.selectors';

import  * as fromTeethActions from '../../../core/store/teeth/tooth.actions';
import { selectTeeth } from '../../../core/store/teeth/tooth.selectors';

import { Country } from '../../../core/enums/country.enum';
import { InvoiceStatus } from '../../../core/enums/invoice-status.enum';

import { CustomValidators } from '../../../core/helpers/custom-validators';
import { EnumHandler } from '../../../core/helpers/enum-handler';
import { MethodHandler } from '../../../core/helpers/method-handler';

import { ProcedureFormGroup } from '../../../core/forms/procedure-form-group.form';
import { ProcedureToothFormGroup } from '../../../core/forms/procedure-tooth-form-group.form';
import { UserFormGroup } from '../../../core/forms/user-form-group.form';

import { ClientApiService } from '../../../core/services/api/client-api.service';
import { InvoiceApiService } from '../../services/invoice-api.service';
import { ClientProcedureApiService } from '../../services/client-procedure-api.service';
import { DoctorApiService } from '../../../core/services/api/doctor-api.service';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { SubscriptionService } from '../../../core/services/shared/subscription.service';
import { SelectModel } from '../../../core/models/view/select-model';
import { UploadFileModel } from '../../../core/models/view/upload-file-model';
import { UploadFileRequest } from '../../../core/models/api/files/upload-file-request';
import { ToothModalModel } from '../../../core/models/view/tooth-modal-model';
import { UploadFileComponent } from '../../../core/components/upload-file/upload-file.component';
import { ToothModalComponent } from '../../../core/components/tooth-modal/tooth-modal.component';

@Component({
  selector: 'app-register-invoice',
  templateUrl: './register-invoice.component.html',
  styleUrls: ['./register-invoice.component.scss']
})
export class RegisterInvoiceComponent implements OnInit {
  @ViewChild(UploadFileComponent) uploadFileComponentRef!: UploadFileComponent

  protected load: boolean = false
  protected clients!: ClientModel[]
  protected countries: SelectModel[] = []
  protected proceduresValues: SelectModel[] = []
  protected clientsValues: Map<string, string> = new Map<string, string>()
  protected teethIdsForm = new FormArray([])
  protected clientFormGroup = new FormGroup<UserFormGroup>({
    name: new FormControl('', [Validators.required, Validators.maxLength(25)]),
    middleName: new FormControl('', [Validators.maxLength(25)]),
    surname: new FormControl('', [Validators.required, Validators.maxLength(25)]),
    secondSurname: new FormControl('', [Validators.maxLength(25)]),
    document: new FormControl('', [Validators.required, Validators.maxLength(15), Validators.min(0), Validators.minLength(6)]),
    ruc: new FormControl({ value: 0, disabled: true}, [Validators.required, Validators.maxLength(1), Validators.min(0), Validators.max(9)]),
    country: new FormControl(Country.Paraguay, [Validators.required]),
    phone: new FormControl('', [Validators.required, Validators.maxLength(15), CustomValidators.checkPhoneValue()]),
    email: new FormControl('', [Validators.maxLength(20), Validators.email]),
    active: new FormControl(true)
  })
  protected uploadFileConfig: UploadFileModel = new UploadFileModel(
    'Subir archivo',
    true,
    'application/pdf,image/jpeg,image/jpg,image/png,image/gif',
    true,
    5000000,
    '275px',
    'm-b-0'
  )
  public saving: boolean = false
  public formGroup = new FormGroup({
    client: this.clientFormGroup,
    procedure: new FormControl(''),
    procedures: new FormArray<FormGroup<ProcedureFormGroup>>([]),
    subTotal: new FormControl({ value: 0, disabled: true}),
    total: new FormControl(0, Validators.min(1)),
    clientId: new FormControl('')
  })
  private procedures!: ProcedureModel[]
  private maxProcedureHeight = 559
  private minProcedureHeight = 532
  protected maximumProcedureHeight = this.maxProcedureHeight
  protected minimumProcedureHeight = this.minProcedureHeight

  constructor(
    private store: Store,
    private readonly clientApiService: ClientApiService,
    private readonly invoiceApiService: InvoiceApiService,
    private readonly clientProcedureApiService: ClientProcedureApiService,
    private readonly doctorApiService: DoctorApiService,
    private userInfoService: UserInfoService,
    private readonly alertService: AlertService,
    private readonly router: Router,
    private readonly subscriptionService: SubscriptionService,
    private modalService: NgbModal,
  ) {
    this.countries = EnumHandler.getCountries()
    this.getScreenSize()
  }

  ngOnInit() {
    this.subscriptionService.onErrorInSave.subscribe({ next: () => { this.saving = false } })
    let loadingClient = true
    const clientRowData$ = this.store.select(selectActiveClients).pipe(tap(x => {
      if(loadingClient && x.length === 0) {
        this.store.dispatch(fromClientsActions.loadClients())
        loadingClient = false
      }
    }))
    let loadingProcedure = true
    const procedureRowData$ = this.store.select(selectActiveProcedures).pipe(tap(x => {
      if(loadingProcedure && x.length === 0) {
        this.store.dispatch(fromProceduresActions.loadProcedures())
        loadingProcedure = false
      }
    }))
    let loadingTeeth = true
    const teethRowData$ = this.store.select(selectTeeth).pipe(tap(x => {
      if(loadingTeeth && x.length === 0) {
        this.store.dispatch(fromTeethActions.componentLoadTeeth())
        loadingTeeth = false
      }
    }))
    combineLatest([clientRowData$, procedureRowData$, teethRowData$]).subscribe({
      next: ([clients, procedures]) => {
        this.clients = clients
        clients.forEach(x => this.clientsValues.set(x.id, x.name))
        this.procedures = procedures
        procedures.forEach(x => this.proceduresValues.push(new SelectModel(x.id, x.name)))
        this.load = true
      }, error: (e) => {
        this.alertService.showError('Error al traer los recuros. si el error persiste contacte con el administrador')
        this.exit()
        throw e
      }
    })
    this.formGroupValueChanges()
    this.formGroup.controls.procedures.addValidators(this.minimumOneSelectedValidator)
  }

  @HostListener('window:resize', ['$event'])
  getScreenSize(event?: any) {
    if (window.screen.height <= 768) {
      this.uploadFileConfig.height = '130px'
      this.maximumProcedureHeight = 407
      this.minimumProcedureHeight = 380
    } else {
      this.uploadFileConfig.height = '285px'
      this.maximumProcedureHeight = this.maxProcedureHeight
      this.minimumProcedureHeight = this.minProcedureHeight
    }
  }

  protected cleanClient = () => {
    this.formGroup.controls.client.reset()
    this.formGroup.controls.client.patchValue({ country: Country.Paraguay })
    this.formGroup.controls.clientId.setValue('', { emitEvent: false })
    this.formGroup.controls.client.enable()
    this.formGroup.controls.client.controls.ruc.disable()
  }

  protected removeProcedure = (id: string) => {
    const formArray = this.formGroup.controls.procedures
    const index = formArray.controls.findIndex(x => x.controls['id'].value === id)
    formArray.removeAt(index)
    this.proceduresValues.find(x => x.key === id)!.disabled = false
    this.calculatePrices()
  }

  protected save = () => {
    if (this.formGroup.invalid) { return }
    this.saving = true
    this.generateRequest().pipe(switchMap((x: InvoiceApiModel) => this.saveFiles(x.id))).subscribe({
      next: () => {
        this.saving = false
        this.alertService.showSuccess('Factura creada con éxito')
        this.formGroup.reset()
        this.exit()
      }, error: (e) => {
        this.saving = false
        throw e
      }
    })
  }

  protected exit = () => {
    const currentUrl = this.router.url.split('/')
    currentUrl.pop()
    this.router.navigate([currentUrl.join('/')])
  }

  protected selectTeeth = (i: number) => {
    const procedure: FormGroup<ProcedureFormGroup> = this.formGroup.controls.procedures.controls[i]
    const modalRef = this.modalService.open(ToothModalComponent, {
      size: 'lg',
      backdrop: 'static',
      keyboard: false
    })
    modalRef.componentInstance.toothIds = procedure.controls.toothIds?.controls.map(x => x.value.id)
    modalRef.result.then((teethIds: ToothModalModel[]) => {
      if (!!teethIds) {
        procedure.controls.toothIds?.clear()
        procedure.controls.teethSelected?.setValue(teethIds.map(x => x.number).sort().join(', '))
        teethIds.forEach(x => {
          procedure.controls.toothIds?.push(
            new FormGroup<ProcedureToothFormGroup>({
              id: new FormControl(x.id)
            })
          )
        })
      }
    }, () => {})
  }

  private formGroupValueChanges = () => {
    this.formGroup.controls.procedure.valueChanges.pipe(
      debounceTime(500),
      filter(x => !!x)
    ).subscribe({
      next: (procedure) => {
        const currentProcedure = this.procedures.find(x => x.id.compareString(procedure!))!
        const formArray = this.formGroup.controls.procedures
        if (!formArray.controls.find((x: FormGroup<ProcedureFormGroup>) => x.controls['id'].value === currentProcedure.id)) {
          const procedureFormGroup = new FormGroup<ProcedureFormGroup>({
            id: new FormControl(currentProcedure.id),
            name: new FormControl(currentProcedure.name),
            price: new FormControl(currentProcedure.price),
            finalPrice: new FormControl(currentProcedure.price, Validators.min(0)),
            xRays: new FormControl(currentProcedure.xRays),
            toothIds: new FormArray<FormGroup<ProcedureToothFormGroup>>([]),
            teethSelected: new FormControl('')
          })
          // procedureFormGroup.addValidators(this.finalPriceCheckValidator)
          formArray.push(procedureFormGroup)
          this.proceduresValues.find(x => x.key === currentProcedure.id)!.disabled = true
          procedureFormGroup.valueChanges.subscribe({
            next: () => this.calculatePrices()
          })
        }
        this.calculatePrices()
        this.formGroup.controls.procedure.setValue('', { onlySelf: true, emitEvent: false })
      }
    })
    this.formGroup.controls.clientId.valueChanges.pipe(
      debounceTime(500),
      filter(x => !!x)
    ).subscribe({
      next: (clientId) => {
        const client = this.clients.find(x => x.id === clientId)
        this.formGroup.controls.client.patchValue({
          name: client!.name,
          middleName: client!.middleName,
          surname: client!.surname,
          secondSurname: client!.secondSurname,
          document: client!.document,
          ruc: client!.ruc,
          phone: client!.phone,
          country: client!.country,
          email: client!.email,
          active: client!.active
        })
        this.formGroup.controls.client.disable()
      }
    })
    this.formGroup.controls.client.controls.document.valueChanges.pipe(
      debounceTime(500),
    ).subscribe({
      next: (document: string | null) => {
        const checkDigit = MethodHandler.calculateCheckDigit(document!, this.formGroup.controls.client.controls.country.value!)
        this.formGroup.controls.client.controls.ruc.setValue(checkDigit)
      }
    })
    this.formGroup.controls.client.controls.country.valueChanges.subscribe(() => this.formGroup.controls.client.controls.document.updateValueAndValidity())
  }

  private calculatePrices = (): void => {
    let subTotal = 0
    let total = 0
    this.formGroup.controls.procedures.controls.forEach(x => {
      subTotal += x.value.price!
      total += x.value.finalPrice!
    })
    this.formGroup.patchValue({
      subTotal: subTotal,
      total: total
    })
  }

  private minimumOneSelectedValidator = (abstractControl: AbstractControl): ValidationErrors | null => {
    const procedures = abstractControl as FormArray<FormGroup<ProcedureFormGroup>>
    return procedures.controls.some(x => x) ? null : { noneSelected : true }
  }

  // private finalPriceCheckValidator = (abstractControl: AbstractControl): ValidationErrors | null => {
  //   const procedure = abstractControl as FormGroup<ProcedureFormGroup>
  //   return (procedure.value.xRays || procedure.value.finalPrice! <= procedure.value.price!) ? null : { invalidFinalPrice : true }
  // }

  private generateRequest = (): Observable<InvoiceApiModel> => {
    const clientId = this.formGroup.value.clientId
    if (clientId) {
      const selectedClient = this.clients.find(x => x.id === clientId)!
      const userId = this.userInfoService.getUserData().id
      if (selectedClient.doctors.find(x => x.id.compareString(userId))) {
        return this.createInvoice(clientId)
      }
      return this.doctorApiService.assignClientToUser(new AssignClientRequest(userId, clientId)).pipe(switchMap(x => this.createInvoice(clientId)))
    }
    // Create Client
    return this.clientApiService.createClient(
      new CreateClientRequest(
        this.formGroup.controls.client.controls.name.value!,
        this.formGroup.controls.client.controls.middleName.value!,
        this.formGroup.controls.client.controls.surname.value!,
        this.formGroup.controls.client.controls.secondSurname.value!,
        this.formGroup.controls.client.controls.phone.value!,
        this.formGroup.controls.client.controls.country.value!,
        this.formGroup.controls.client.controls.email.value!,
        this.formGroup.controls.client.controls.document.value!,
        this.formGroup.controls.client.controls.ruc.value!.toString()
      )
    ).pipe(switchMap(client => this.createInvoice(client.id)))
  }

  private createInvoice = (clientId: string): Observable<InvoiceApiModel> => {
    // Create Client Procedure
    const procedures = this.formGroup.controls.procedures.controls
    const clientProcedures$ = procedures.map(x => this.clientProcedureApiService.createClientProcedure(new CreateClientProcedureRequest(x.controls.id.value!, clientId)))
    return forkJoin(clientProcedures$).pipe(switchMap(clientProcedures => {
      // Create Invoice
      const invoiceDetails: CreateInvoiceDetailRequest[] = clientProcedures.map(x => {
        const selectedProcedure = procedures.find(y => y.controls.id.value === x.procedureId)!
        const toothIds = selectedProcedure.controls.toothIds?.controls.map(x => x.controls.id.value!)
        return new CreateInvoiceDetailRequest(
          x.id,
          selectedProcedure.controls.price.value!,
          selectedProcedure.controls.finalPrice.value!,
          toothIds
        )
      })
      const invoice = new CreateInvoiceRequest(
        'invoiceNumber',
        1,
        1,
        this.formGroup.controls.subTotal.value!,
        this.formGroup.controls.total.value!,
        'timbrado',
        InvoiceStatus.Nuevo,
        clientId,
        invoiceDetails
      )
      return this.invoiceApiService.createInvoice(invoice)
    }))
  }

  private saveFiles = (invoiceId: string): Observable<string[]> => {
    const files = this.uploadFileComponentRef.files
    if (files.length > 0) {
      const request = new UploadFileRequest(invoiceId, this.uploadFileComponentRef.files)
      return this.invoiceApiService.uploadInvoiceFiles(request)
    }
    return of(new Array<string>())
  }
}
