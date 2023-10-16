import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { AccountInfo, EventMessage, EventType, IPublicClientApplication, InteractionStatus } from '@azure/msal-browser';
import { Observable, filter, map, switchMap } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { AuthApiService } from '../../services/api/auth-api.service';
import { UserInfoService } from '../../services/shared/user-info.service';
import { RoleApiService } from '../../services/api/role-api.service';
import { UserApiModel } from '../../models/users/api/user-api-model';
import { LoginRequest } from '../../models/users/api/login-request';

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
    // this.msalBroadcastService.msalSubject$.pipe(
    //   filter(x => x.eventType === EventType.LOGIN_FAILURE || x.eventType === EventType.ACQUIRE_TOKEN_FAILURE)).subscribe({
    //   next: (result) => {
    //     // Checking for the forgot password error. Learn more about B2C error codes at
    //     // https://learn.microsoft.com/azure/active-directory-b2c/error-codes
    //     if (result.error && result.error.message.indexOf('AADB2C90118') > -1) {
    //       const resetPasswordFlowRequest = {
    //         authority: `https://${environment.hostName}/${environment.domainName}/${environment.resetPasswordPolicyName}`,
    //         scopes: environment.endpointScopes,
    //       }

    //       this.authApiService.login(resetPasswordFlowRequest as unknown as LoginRequest).subscribe()
    //     }
    //   }, error: (e) => {
    //     console.log(e)
    //     throw e
    //   }
    // })
    this.msalBroadcastService.msalSubject$.subscribe((event: EventMessage) => {
      if (event.eventType === EventType.LOGIN_FAILURE) {
        if (event?.error?.message.includes('AADB2C90118')) {
          console.log('El usuario canceló el inicio de sesión.');
          // Aquí puedes realizar las acciones que consideres apropiadas cuando el usuario cancele el inicio de sesión.
        }
      }
    });
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
}
