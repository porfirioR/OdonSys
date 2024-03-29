import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngrx/store';
import { combineLatest, debounceTime, map, of, switchMap, take } from 'rxjs';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { PaymentApiService } from '../../services/payment-api.service';
import { InvoiceApiModel } from '../../models/invoices/api/invoice-api-model';
import { PaymentApiModel } from '../../models/payments/payment-api-model';
import { PaymentRequest } from '../../models/payments/payment-request';
import { PaymentModel } from '../../models/payments/payment-model';
import  * as fromDoctorsActions from '../../../core/store/doctors/doctor.actions';
import { selectDoctor } from '../../../core/store/doctors/doctor.selectors';

@Component({
  selector: 'app-payment-modal',
  templateUrl: './payment-modal.component.html',
  styleUrls: ['./payment-modal.component.scss']
})
export class PaymentModalComponent implements OnInit {
  @Input() invoice!: InvoiceApiModel
  public formGroup = new FormGroup({
    total: new FormControl(0),
    remainingDebt: new FormControl(0),
    amount: new FormControl(1, [Validators.required, Validators.min(0)]),
    totalPaid: new FormControl(0)
  })
  protected payments: PaymentModel[] = []
  protected saving = false
  protected loading = true
  protected hasPayments = false

  constructor(
    public activeModal: NgbActiveModal,
    private userInfoService: UserInfoService,
    private alertService: AlertService,
    private readonly paymentApiService: PaymentApiService,
    private readonly store: Store,
  ) { }

  ngOnInit() {
    this.paymentApiService.getPaymentsByInvoiceId(this.invoice.id).pipe(switchMap((payments: PaymentApiModel[]) => {
      const remainingDebt = this.invoice.total - payments.reduce((a, b) => a + b.amount, 0)
      this.formGroup.patchValue({
        total: this.invoice.total,
        remainingDebt: remainingDebt
      })
      this.formGroup.controls.amount.addValidators(Validators.max(remainingDebt))
      this.hasPayments = payments.length > 0
      const userPayments = [... new Set(payments.map(x => x.userId))]
      const userPayments$ = userPayments.map(userId => {
        this.store.dispatch(fromDoctorsActions.loadDoctor({ doctorId: userId }))
        return this.store.select(selectDoctor(userId)).pipe(debounceTime(500))
      })
      return userPayments$.length > 0 ? combineLatest(userPayments$).pipe(take(1), map(users => {
        this.payments = []
        let remainingDebt = this.invoice.total
        payments.forEach(payment => {
          const user = users.find(x => x!.id.compareString(payment.userId))
          remainingDebt -= payment.amount
          const paymentItem = new PaymentModel(user!.userName, payment.dateCreated, payment.amount, remainingDebt)
          this.payments.push(paymentItem)
        })
        const totalPaid = this.payments.reduce((sum, currentObject) => sum + currentObject.amount, 0)
        this.formGroup.controls.totalPaid.setValue(totalPaid)
        this.loading = false
      })) : of(this.loading = false)
    })).subscribe({
      error: (e) => {
        this.loading = false
        throw e
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
