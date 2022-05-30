import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfoService } from '../../services/shared/user-info.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(private readonly userInfoService: UserInfoService, private readonly router: Router) { }

  ngOnInit() {
  }

  public logOut = () => {
    this.userInfoService.clearAll();
    this.router.navigateByUrl('/login');
  }
}
