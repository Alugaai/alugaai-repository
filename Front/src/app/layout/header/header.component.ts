import { AfterViewInit, Component, OnInit, DoCheck , ChangeDetectorRef} from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import { IUserDetails } from '../../_models/IUserDetails';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { NotificationService } from '../../_services/notification.service';
import { IStudentsWhoInvitationsConnections } from '../../_models/IStudentsWhoInvitationsConnections';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements AfterViewInit, OnInit {
  [x: string]: any;
  userLogged: boolean = false;
  userRole: Array<string> = [];
  email: string = '';
  userDetails?: IUserDetails;
  userImage: SafeUrl = '';
  base64: string = 'data:image/png;base64,';
  notificationCounter: any; //Puxar a notificação do banco e zerar todas vez q o user abrir elas
  studentsWhoInvitationsConnections: Array<IStudentsWhoInvitationsConnections> =
    [];
  badgeHidden: boolean = false;
  isMenuOpen: boolean = false;

  constructor(
    private authService: AuthService,
    private sanitizer: DomSanitizer,
    private router: Router,
    private notificationServer: NotificationService,
    private cd: ChangeDetectorRef
  ) {
    this.authService.userLoggedToken$.subscribe({
      next: (userToken) => {
        this.userLogged = userToken !== null ? true : false;
        if (userToken?.role) {
          this.userRole = userToken.role;
        }
        if (userToken?.email) {
          this.email = userToken.email;
          this.cd.detectChanges()
        }
      },
    });
  }
  ngOnInit(): void {
    this.findUserDetails();
    if (this.userLogged) {
      this.getStudentsWhoInvitationsConnections();
    }
  }

  ngAfterViewInit(): void {
    this.findUserDetails();
    if (this.userLogged) {
      this.getStudentsWhoInvitationsConnections();
    }
  }

  findUserDetails() {
    this.authService.userDetailsByEmail(this.email).subscribe({
      next: (response) => {
        this.userDetails = response;
        this.startImage();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  startImage() {
    if (this.userDetails?.imageUser) {
      let image = this.userDetails?.imageUser.imageData64;
      this.userImage = this.sanitizer.bypassSecurityTrustUrl(
        this.base64 + image
      );
    }
  }

  // Função que puxa as notificações do banco e zera o contador
  countNotifications() {
    this.notificationServer.getNotifications().subscribe({
      next: (response) => {
        this.notificationCounter = response;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  getStudentsWhoInvitationsConnections() {
    this.notificationServer.getStudentsWhoInvitationsConnections().subscribe({
      next: (response) => {
        this.studentsWhoInvitationsConnections = response.result;
        this.countNotifications();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigateByUrl('/auth/login', {});
  }

  //Função que deixa a badge invisivel (se o user clicar ela fica invisivel, seta a variavel pra true e zera o numero de notificacao)
  toggleBadgeVisibility() {
    if (this.notificationCounter > 0) {
      this.badgeHidden;
    } else {
      !this.badgeHidden;
    }
  }

  goToPerfil() {
    this.router.navigateByUrl('/perfil/estudante', {});
  }

  onMenuOpened() {
    this.isMenuOpen = true;
  }

  onMenuClosed() {
    this.isMenuOpen = false;
  }

  onMenuItemClick() {
    this.isMenuOpen = true;
  }
}
