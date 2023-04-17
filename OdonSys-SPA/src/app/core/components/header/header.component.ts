import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfoService } from '../../services/shared/user-info.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  protected userName: string = ''

  constructor(
    private readonly userInfoService: UserInfoService,
    private readonly router: Router
  ) { }

  ngOnInit() {
    this.userName = this.userInfoService.getUserData().userName
  }

  public logOut = () => {
    this.userInfoService.clearAllCredentials()
    this.router.navigateByUrl('/login')
  }
}
