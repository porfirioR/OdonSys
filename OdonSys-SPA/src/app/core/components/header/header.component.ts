import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfoService } from '../../services/shared/user-info.service';
import { Permission } from '../../enums/permission.enum';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  protected userName: string = ''
  protected canAccessData = false

  constructor(
    private readonly userInfoService: UserInfoService,
    private readonly router: Router
  ) {
    this.canAccessData = userInfoService.havePermission(Permission.AccessDoctors)

  }

  ngOnInit() {
    this.userName = this.userInfoService.getUserData().userName
  }

  public logOut = () => {
    this.userInfoService.clearAllCredentials()
    this.router.navigateByUrl('/login')
  }
}
