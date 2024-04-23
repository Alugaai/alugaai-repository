import { AfterViewInit, Component, OnInit } from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import { IUserDetailsByEmail } from '../../_models/IUserDetailsByEmail';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements AfterViewInit {
  userLogged: boolean = false;
  userRole: Array<string> = [];
  email: string = '';
  userDetails?: IUserDetailsByEmail;
  userImage: SafeUrl = '';
  base64: string = 'data:image/png;base64,';
  notificationCounter: number = 10; //Puxar a notificação do banco e zerar todas vez q o user abrir elas
  badgeHidden: boolean = false;

  constructor(private authService: AuthService, private sanitizer: DomSanitizer) {
    this.authService.userLoggedToken$.subscribe({
      next: (userToken) => {
        this.userLogged = userToken !== null;
        if (userToken?.role) {
          this.userRole = userToken.role;
        }
        if (userToken?.email) {
          this.email = userToken.email;
        }
      }
    });
  }
  ngAfterViewInit(): void {
    this.findUserDetailsByEmail();
    this.startImage();
  }



  findUserDetailsByEmail() {
    this.authService.userDetailsByEmail(this.email).subscribe({
      next: (response) => {
        this.userDetails = response;
        this.startImage();
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  startImage() {
    if (this.userDetails!.imageUser) {
      let image = this.userDetails?.imageUser.imageData64;
      this.userImage = this.sanitizer.bypassSecurityTrustUrl(
        this.base64 + image
      );
    }
  }
  //Função que deixa a badge invisivel (se o user clicar ela fica invisivel, seta a variavel pra true e zera o numero de notificacao)
  toggleBadgeVisibility() {
    this.badgeHidden = !this.badgeHidden;
  }

}

