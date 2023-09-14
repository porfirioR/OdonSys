import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClientApiService } from '../../services/api/client-api.service';

@Component({
  selector: 'app-client-report',
  templateUrl: './client-report.component.html',
  styleUrls: ['./client-report.component.scss']
})
export class ClientReportComponent implements OnInit {
  protected loading = true
  private id: string

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly clientApiService: ClientApiService
  ) { }

  ngOnInit() {
    this.id = this.activatedRoute.snapshot.params['id']
    this.clientApiService.getClientReport(this.id).subscribe({
      next: (value) => {
        
        this.loading = false
      }, error: (e) => {
        this.loading = false
        throw e
      }
    })
  }

}
