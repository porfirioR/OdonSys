import { Component, OnInit } from '@angular/core';
import { GridOptions } from 'ag-grid-community';
import { AlertService } from 'src/app/core/services/shared/alert.service';

@Component({
  selector: 'app-admin-clients',
  templateUrl: './admin-clients.component.html',
  styleUrls: ['./admin-clients.component.css']
})
export class AdminClientsComponent implements OnInit {
  public loading: boolean = false;
  public ready: boolean = false;
  public gridOptions!: GridOptions;

  constructor(
    private readonly alertService: AlertService,

  ) { }

  ngOnInit() {
  }

}
