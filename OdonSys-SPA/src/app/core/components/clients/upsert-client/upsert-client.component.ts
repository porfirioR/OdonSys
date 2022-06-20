import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-upsert-client',
  templateUrl: './upsert-client.component.html',
  styleUrls: ['./upsert-client.component.scss']
})
export class UpsertClientComponent implements OnInit {
  public title: string = 'Nuevo paciente';
  constructor() { }

  ngOnInit() {
  }

}
