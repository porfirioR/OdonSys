import { Component, OnInit } from '@angular/core';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { AccountInfo, EventMessage, EventType, IPublicClientApplication, InteractionStatus } from '@azure/msal-browser';
import { Observable, filter, map, of, switchMap } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { AuthApiService } from '../../services/api/auth-api.service';
import { RegisterUserRequest } from '../../models/users/api/register-user-request';
import { Country } from '../../enums/country.enum';

@Component({
  selector: 'app-principal-page',
  templateUrl: './principal-page.component.html',
  styleUrls: ['./principal-page.component.scss']
})
export class PrincipalPageComponent implements OnInit {
  public userData: any = {}
  protected loaded = false

  constructor(
    private msalBroadcastService: MsalBroadcastService,
    private msalService: MsalService,
    private authApiService: AuthApiService
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
    )
    account.pipe(
      switchMap(accountInfo => {
        const isNewUser = accountInfo?.idTokenClaims?.[environment.newUserKey] as boolean;
        return isNewUser ? 
          this.authApiService.registerAadB2C() :
          this.authApiService.getProfile(accountInfo!.localAccountId)
      }
    )).subscribe({
      next: (accountInfo) => {
        this.loaded = true
        console.log(accountInfo)
        // this.userData.id = accountInfo?.localAccountId;
        // this.userData.name = accountInfo?.name;
        // this.userData.email = accountInfo?.username;
        // this.loaded = true;
      },
      error: (e) => {
        // this.loaded = true
        throw e
      }
    })
  }

  ngOnInit() {
  }

}
