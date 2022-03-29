/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { AdminProcedureComponent } from './admin-procedure.component';

describe('AdminProcedureComponent', () => {
  let component: AdminProcedureComponent;
  let fixture: ComponentFixture<AdminProcedureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
    declarations: [AdminProcedureComponent],
    teardown: { destroyAfterEach: false }
})
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminProcedureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
