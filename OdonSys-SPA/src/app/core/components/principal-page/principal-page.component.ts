import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { AccountInfo, EventType, IPublicClientApplication, InteractionStatus } from '@azure/msal-browser';
import { Observable, filter, map, switchMap } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { AuthApiService } from '../../services/api/auth-api.service';
import { UserInfoService } from '../../services/shared/user-info.service';
import { RoleApiService } from '../../services/api/role-api.service';
import { UserApiModel } from '../../models/users/api/user-api-model';

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
    private authApiService: AuthApiService,
    private userInfoService: UserInfoService,
    private readonly roleApiService: RoleApiService,
    private readonly router: Router
  ) { }

  ngOnInit() {
    this.msalBroadcastService.msalSubject$.pipe(filter(x => x.eventType === EventType.LOGIN_FAILURE || x.eventType === EventType.ACQUIRE_TOKEN_FAILURE)).subscribe({
      next: (result) => {
        // Checking for the forgot password error. Learn more about B2C error codes at
            // https://learn.microsoft.com/azure/active-directory-b2c/error-codes
          //   if (result.error && result.error.message.indexOf('AADB2C90118') > -1) {
          //     let resetPasswordFlowRequest: RedirectRequest | PopupRequest = {
          //         authority: b2cPolicies.authorities.resetPassword.authority,
          //         scopes: [],
          //     };

          //     this.authApiService.login(resetPasswordFlowRequest);
          // };
      }, error: (e) => {
        console.log(e)
        throw e
      }
    })
    const accountInfo$: Observable<AccountInfo | null> = this.msalBroadcastService.inProgress$.pipe(
      filter((status) => status == InteractionStatus.None),
      map(() => {
        const instance: IPublicClientApplication = this.msalService.instance;
        const activeAccount: AccountInfo | null = instance.getActiveAccount();
        if (activeAccount != null) {
          return activeAccount
        }
        const accounts: AccountInfo[] = instance.getAllAccounts();
        if (accounts.length > 0) {
          const [firstAccount] = accounts
          instance.setActiveAccount(firstAccount)
          return firstAccount
        }
        return null
      })
    )
    accountInfo$.pipe(
      switchMap(accountInfo => {
        const isNewUser = accountInfo?.idTokenClaims?.[environment.newUserKey] as boolean;
        return isNewUser ?
          this.authApiService.registerAadB2C() :
          this.authApiService.getProfile()
      }
    )).pipe(switchMap((x: UserApiModel) => {
      this.userInfoService.setUser(x)
      if (!x.approved) {
        this.router.navigate(['sin-autorización'])
      }
      return this.roleApiService.getMyPermissions()
    })).subscribe({
      next: (permissions: string[]) => {
        this.userInfoService.setUserPermissions(permissions)
        this.loaded = true
      },
      error: (e) => {
        throw e
      }
    })
  }


  protected resetPassword = () => {
    const resetPasswordUrl = 'https://odonsystem.b2clogin.com/odonsystem.onmicrosoft.com/B2C_1_ResetPassword';
    this.msalService.loginRedirect( {redirectUri: resetPasswordUrl, scopes: [],} )
  }
}
