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

@Component({
  selector: 'app-principal-page',
  templateUrl: './principal-page.component.html',
  styleUrls: ['./principal-page.component.scss']
})
export class PrincipalPageComponent implements OnInit {
  protected loaded = false

  constructor(
    private readonly msalBroadcastService: MsalBroadcastService,
    private readonly msalService: MsalService,
    private readonly authApiService: AuthApiService,
    private readonly userInfoService: UserInfoService,
    private readonly roleApiService: RoleApiService,
    private readonly router: Router
  ) { }

  ngOnInit() {
    const accountInfo$: Observable<AccountInfo | null> = this.msalBroadcastService.inProgress$.pipe(
      filter((status) => status == InteractionStatus.None),
      map(() => {
        const instance: IPublicClientApplication = this.msalService.instance
        const activeAccount: AccountInfo | null = instance.getActiveAccount()
        if (activeAccount != null) {
          return activeAccount
        }
        const accounts: AccountInfo[] = instance.getAllAccounts()
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
        const isNewUser = accountInfo?.idTokenClaims?.[environment.newUserKey] as boolean
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
      }
    })
  }
}
