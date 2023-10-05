import { Component, OnInit } from '@angular/core';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { AccountInfo, EventMessage, EventType, IPublicClientApplication, InteractionStatus } from '@azure/msal-browser';
import { Observable, filter, map, of, switchMap } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { AuthApiService } from '../../services/api/auth-api.service';
import { RegisterUserRequest } from '../../models/users/api/register-user-request';
import { Country } from '../../enums/country.enum';
import { UserInfoService } from '../../services/shared/user-info.service';
import { RoleApiService } from '../../services/api/role-api.service';

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
    private readonly roleApiService: RoleApiService
  ) { }

  ngOnInit() {
    const accountInfo$: Observable<AccountInfo | null> =
    this.msalBroadcastService.inProgress$.pipe(
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
    )).pipe(switchMap(x => {
      this.userInfoService.setUser(x)
      return this.roleApiService.getMyPermissions()
    })).subscribe({
      next: (permissions) => {
        this.userInfoService.setUserPermissions(permissions)
        this.loaded = true
      },
      error: (e) => {
        throw e
      }
    })
  }

}
