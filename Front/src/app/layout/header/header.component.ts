import { Component } from '@angular/core';
import { AuthService } from '../../_services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  userLogged: boolean = false;
  userRole: Array<string> = [];

  constructor(private authService: AuthService) {
    this.authService.userLoggedToken$.subscribe({
      next: (userToken) => {
        this.userLogged = userToken !== null;
        if (userToken?.role) {
          this.userRole = userToken.role;
        }
      }
    });
  }
}

