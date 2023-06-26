import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfoService } from '../../services/shared/user-info.service';
import { MenuService } from '../../services/shared/menu.service';
import { SubscriptionService } from '../../services/shared/subscription.service';
import { Permission } from '../../enums/permission.enum';
import { MenuItem } from '../../models/view/menu-item';
import { AuthApiService } from '../../services/api/auth-api.service';
import { LogoutApiModel } from '../../models/users/api/logout-api-model';
import { AlertService } from '../../services/shared/alert.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  protected userName: string = ''
  protected canAccessData = false
  protected menuPermissions: MenuItem[] = []
  protected reloadHeader = false
  constructor(
    private readonly userInfoService: UserInfoService,
    private readonly router: Router,
    private readonly subscriptionService: SubscriptionService,
    private readonly zone: NgZone,
    private readonly authApiService: AuthApiService,
    private readonly alertService: AlertService
  ) {
    this.checkMenuItems()
    this.subscriptionService.onCheckUpdateMenu.subscribe(this.checkMenuItems)
  }

  ngOnInit() {
    this.userName = this.userInfoService.getUserData().userName
  }

  public logOut = () => {
    this.authApiService.logout().subscribe({
      next: (logout: LogoutApiModel) => {
        this.userInfoService.clearAllCredentials()
        this.alertService.showSuccess(`Hasta pronto: ${logout.username}`)
        this.zone.run(() => this.router.navigateByUrl('/login'))
      }
    })
  }

  private checkMenuItems = () => {
    this.canAccessData = this.userInfoService.havePermission(Permission.AccessMyData)
    const permissions = this.userInfoService.getPermissions()
    this.menuPermissions = MenuService.getPrincipalItems().filter(x => permissions.includes(x.permission))
  }
}
