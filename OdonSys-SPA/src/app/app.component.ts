import { Component } from '@angular/core';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { EventMessage, EventType } from '@azure/msal-browser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private authService: MsalService, private msalBroadcastService: MsalBroadcastService) {
    this.msalBroadcastService.msalSubject$.subscribe((event: EventMessage) => {
      if (event.eventType === EventType.LOGIN_FAILURE) {
        if (event?.error?.message.includes('AADB2C90118')) {
          console.log('El usuario canceló el inicio de sesión.');
          // Aquí puedes realizar las acciones que consideres apropiadas cuando el usuario cancele el inicio de sesión.
        }
      }
    });
  }
}
