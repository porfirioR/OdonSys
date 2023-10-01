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
        return this.authApiService.getProfile()
        // const isNewUser = accountInfo?.idTokenClaims?.[environment.newUserKey] as boolean;

        // if (isNewUser) {
        //   const newUser = new RegisterUserRequest(
        //     accountInfo!.idTokenClaims?.['given_name'] as string,
        //     accountInfo!.idTokenClaims?.['family_name'] as string,
        //     accountInfo!.idTokenClaims?.['extension_Document'] as string,
        //     '',
        //     accountInfo!.idTokenClaims?.['extension_Phone'] as string,
        //     accountInfo!.username,
        //     accountInfo!.idTokenClaims?.['country'] as Country,
        //     accountInfo!.idTokenClaims?.['extension_Document'] as string,
        //     accountInfo!.idTokenClaims?.['extension_SecondName'] as string,
        //     accountInfo!.localAccountId,
        //     accountInfo!.name
        //   )
        //   return this.authApiService.registerAadB2C(newUser)
        //   //TODO Go Backend and load data
        //   // msalService.logout().subscribe()
        // }
        // return of()
      }
    )).subscribe({
      next: (accountInfo) => {
        
        console.log(accountInfo)
        // this.userData.id = accountInfo?.localAccountId;
        // this.userData.name = accountInfo?.name;
        // this.userData.email = accountInfo?.username;
        // this.loaded = true;
      },
      error: (e) => {
        throw e
      }
    })
  }

  ngOnInit() {
  }

}
