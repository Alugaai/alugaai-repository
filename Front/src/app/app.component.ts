import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { IUserToken } from './_models/IUserToken';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  constructor(private authService: AuthService) {}

  setUserLogged() {
    let userString;
    if (typeof localStorage != 'undefined') {
      userString = localStorage.getItem('user');
    }
    if (!userString) {
      return;
    }

    const userToken: IUserToken = JSON.parse(userString);
    this.authService.setUserLogged(userToken);
  }

  ngOnInit(): void {
    this.setUserLogged();
  }

  title = 'my-angular-app';
}
