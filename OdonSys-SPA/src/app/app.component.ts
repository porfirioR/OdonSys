import { Component } from '@angular/core';
import { MsalBroadcastService } from '@azure/msal-angular';
import { EventMessage, EventType } from '@azure/msal-browser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private msalBroadcastService: MsalBroadcastService) {
    this.msalBroadcastService.msalSubject$.subscribe((event: EventMessage) => {
      if (event.eventType === EventType.LOGIN_FAILURE && event?.error?.message.includes('AADB2C90091')) {
        window.location.reload()
      }
    })
  }
}
