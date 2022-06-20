import { Component, OnInit } from '@angular/core';
import { ClientApiService } from '../../services/api/client-api.service';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.scss']
})
export class ClientsComponent implements OnInit {

  constructor(
    private readonly clientsApiServices: ClientApiService
  ) { }

  ngOnInit() {
  }

}
