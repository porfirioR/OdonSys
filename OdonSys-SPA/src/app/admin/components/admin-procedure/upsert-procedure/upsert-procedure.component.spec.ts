/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UpsertProcedureComponent } from './upsert-procedure.component';

describe('UpsertProcedureComponent', () => {
  let component: UpsertProcedureComponent;
  let fixture: ComponentFixture<UpsertProcedureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpsertProcedureComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpsertProcedureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
