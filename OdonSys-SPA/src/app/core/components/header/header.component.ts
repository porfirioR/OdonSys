import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfoService } from '../../services/shared/user-info.service';
import { Permission } from '../../enums/permission.enum';
import { MenuItem } from '../../models/view/menu-item';
import { MenuService } from '../../services/shared/menu.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  protected userName: string = ''
  protected canAccessData = false
  protected menuPermissions: MenuItem[] = []

  constructor(
    private readonly userInfoService: UserInfoService,
    private readonly router: Router
  ) {
    this.canAccessData = userInfoService.havePermission(Permission.AccessDoctors)
    const permissions = userInfoService.getPermissions()
    this.menuPermissions = MenuService.getPrincipalItems().filter(x => permissions.includes(x.permission))
  }

  ngOnInit() {
    this.userName = this.userInfoService.getUserData().userName
  }

  public logOut = () => {
    this.userInfoService.clearAllCredentials()
    this.router.navigateByUrl('/login')
  }
}
