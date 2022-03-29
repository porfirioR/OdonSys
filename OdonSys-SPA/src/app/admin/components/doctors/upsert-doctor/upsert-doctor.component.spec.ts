/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UpsertDoctorComponent } from './upsert-doctor.component';

describe('UpsertDoctorComponent', () => {
  let component: UpsertDoctorComponent;
  let fixture: ComponentFixture<UpsertDoctorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
    declarations: [UpsertDoctorComponent],
    teardown: { destroyAfterEach: false }
})
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpsertDoctorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
