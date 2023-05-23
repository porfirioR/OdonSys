import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { combineLatest, take } from 'rxjs';
import { UserRoleApiRequest } from '../../../core/models/api/roles/user-role-api-request';
import { CheckFormGroup } from '../../../core/forms/check-form-group';
import { RoleApiService } from '../../../core/services/api/role-api.service';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { DoctorApiService } from '../../../core/services/api/doctor-api.service';
import { PaymentApiService } from '../../services/payment-api.service';
import { InvoiceApiModel } from '../../models/invoices/api/invoice-api-model';
import { PaymentApiModel } from '../../models/payments/payment-api-model';
import { PaymentRequest } from '../../models/payments/payment-request';

@Component({
  selector: 'app-payment-modal',
  templateUrl: './payment-modal.component.html',
  styleUrls: ['./payment-modal.component.scss']
})
export class PaymentModalComponent implements OnInit {
  @Input() invoice!: InvoiceApiModel
  protected payments: PaymentApiModel[] = []
  protected saving = false
  public formGroup = new FormGroup( {
    total: new FormControl(0),
    remainingDebt: new FormControl(0),
    amount: new FormControl(0, [Validators.required, Validators.min(0)])
  })

  constructor(
    public activeModal: NgbActiveModal,
    private readonly rolesApiService: RoleApiService,
    private userInfoService: UserInfoService,
    private alertService: AlertService,
    private readonly doctorApiService: DoctorApiService,
    private readonly paymentApiService: PaymentApiService
  ) { }

  ngOnInit() {
    this.paymentApiService.getPaymentsByInvoiceId(this.invoice.id).subscribe({
      next: (payments: PaymentApiModel[]) => {
        this.payments = payments
        this.formGroup.controls.total.setValue(this.invoice.total)
        const remainingDebt = this.invoice.total - payments.reduce((a, b) => a + b.amount, 0)
        this.formGroup.controls.remainingDebt.setValue(remainingDebt)
        this.formGroup.controls.amount.addValidators(Validators.max(remainingDebt))
      }, error: (e) => {
        this.activeModal.dismiss()
        throw e;
      }
    })
  }

  protected save = () => {
    this.saving = true
    const request = new PaymentRequest(this.invoice.id, this.userInfoService.getUserData().id, this.formGroup.value.amount!)
    this.paymentApiService.registerPayment(request).pipe(take(1)).subscribe({
      next: (payment: PaymentApiModel) => {
        this.alertService.showSuccess('Pago registrado correctamente.')
        this.saving = false
        this.activeModal.close(payment)
      }, error: (e) => {
        this.saving = false
        throw e
      }
    })
  }

}
