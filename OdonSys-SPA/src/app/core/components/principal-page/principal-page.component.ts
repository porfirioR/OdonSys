import { Component, OnInit } from '@angular/core';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { AccountInfo, EventMessage, EventType, IPublicClientApplication, InteractionStatus } from '@azure/msal-browser';
import { Observable, filter, map } from 'rxjs';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-principal-page',
  templateUrl: './principal-page.component.html',
  styleUrls: ['./principal-page.component.scss']
})
export class PrincipalPageComponent implements OnInit {
  public userData: any = {}

  constructor(
    private msalBroadcastService: MsalBroadcastService,
    private msalService: MsalService
  ) {
    const account: Observable<AccountInfo | null> =
    this.msalBroadcastService.inProgress$.pipe(
      filter((status) => status == InteractionStatus.None),
      map(() => {
        const instance: IPublicClientApplication = this.msalService.instance;
        const activeAccount: AccountInfo | null = instance.getActiveAccount();
        if (activeAccount != null) {
          return activeAccount;
        }
        const accounts: AccountInfo[] = instance.getAllAccounts();
        if (accounts.length > 0) {
          const [firstAccount] = accounts;
          instance.setActiveAccount(firstAccount);
          return firstAccount;
        }
        return null;
      })
    );
  account.subscribe({
    next: (accountInfo) => {
      const isNewUser = accountInfo?.idTokenClaims?.[
        environment.newUserKey
      ] as boolean;
      if (isNewUser) {
        //TODO Go Backend and load data
        // msalService.logout().subscribe()
      }
      this.userData.id = accountInfo?.localAccountId;
      this.userData.name = accountInfo?.name;
      this.userData.email = accountInfo?.username;
      // this.loaded = true;
    },
    error: (e) => {
      throw e;
    },
  });
    
  }

  ngOnInit() {
    // this.msalBroadcastService.msalSubject$.pipe(
    //   filter((msg: EventMessage) => msg.eventType === EventType.LOGIN_SUCCESS)
    // ).subscribe({
    //   next: (value: EventMessage) => {
    //     const accountInfo: AccountInfo = value.payload as AccountInfo
    //     const isNewUser = accountInfo.idTokenClaims?.[environment.newUserKey] as boolean
    //     if (isNewUser) {
    //       this.msalService.logout().subscribe()
    //     }
    //   }, error: (e) => {
    //     throw e
    //   }
    // })

    // this.msalBroadcastService.msalSubject$
    //   .pipe(
    //     filter((msg: EventMessage) => msg.eventType === EventType.ACCOUNT_ADDED || msg.eventType === EventType.ACCOUNT_REMOVED),
    //   )
    //   .subscribe((result: EventMessage) => {
    //     if (this.msalService.instance.getAllAccounts().length === 0) {
    //       window.location.pathname = "/";
    //     } else {
    //       // this.setLoginDisplay();
    //     }
    //   });
    
    // this.msalBroadcastService.inProgress$
    //   .pipe(
    //     filter((status: InteractionStatus) => status === InteractionStatus.None),
    //   )
    //   .subscribe(() => {
    //     // this.setLoginDisplay();
    //     this.checkAndSetActiveAccount();
    //   })
  }

  private checkAndSetActiveAccount = () => {
    /**
     * If no active account set but there are accounts signed in, sets first account to active account
     * To use active account set here, subscribe to inProgress$ first in your component
     * Note: Basic usage demonstrated. Your app may require more complicated account selection logic
     */
    // let activeAccount = this.msalService.instance.getActiveAccount();

    // if (!activeAccount && this.msalService.instance.getAllAccounts().length > 0) {
    //   let accounts = this.msalService.instance.getAllAccounts();
    //   this.msalService.instance.setActiveAccount(accounts[0]);
    // }
  }
}
