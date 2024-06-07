import { Component, OnInit} from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import { IUserDetails } from '../../_models/IUserDetails';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { NotificationService } from '../../_services/notification.service';
import { IStudentsWhoInvitationsConnections } from '../../_models/IStudentsWhoInvitationsConnections';
import { Subscription } from 'rxjs';
import { ComponentUpdateService } from '../../_services/component-update.service';
import { NotificationUpdateService } from '../../_services/notification-update.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {
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


  private userTokenSubscription: Subscription | undefined;
  // atualização dinamica escutando o filho
  private noticatonUpdateSubscription: Subscription | undefined;


  constructor(
    private authService: AuthService,
    private sanitizer: DomSanitizer,
    private router: Router,
    private notificationServer: NotificationService,
    private noticatonUpdateService: NotificationUpdateService,
    private componentUpdateService: ComponentUpdateService
  ) {
  }
  ngOnInit(): void {
    this.userTokenSubscription = this.authService.userLoggedToken$.subscribe(userToken => {
      this.userLogged = !!userToken;
      if (userToken) {
        this.userRole = userToken.role;
        if (this.userRole.includes('Student' || 'Admin')) {
          this.countNotifications();
        this.getStudentsWhoInvitationsConnections();
        }
        this.email = userToken.email;
        this.findUserDetails();
        // atualização dinamica escutando o filho
        if (this.userRole.includes('Student' || 'Admin')) {
          this.noticatonUpdateSubscription = this.noticatonUpdateService.notificationUpdate$.subscribe(() => {
            this.countNotifications();
            this.getStudentsWhoInvitationsConnections();
          });
        }
        //
      } else {
        this.clearUserData();
      }
    });
  }

  ngOnDestroy(): void {
    if (this.userTokenSubscription) {
      this.userTokenSubscription.unsubscribe();
    }
    // atualização dinamica escutando o filho, destruindo a comunicação
    if (this.noticatonUpdateSubscription) {
      this.noticatonUpdateSubscription.unsubscribe();
    }

  }

  clearUserData(): void {
    this.userDetails = undefined; // reset user details
    this.userImage = ''; // Reset user image
    this.email = ''; // Reset email
    this.notificationCounter = 0; // Reset notification counter
    this.studentsWhoInvitationsConnections = []; // Reset connections
    this.userRole = [];
  }



  findUserDetails() {
    this.authService.userDetailsByEmail(this.email).subscribe({
      next: (response) => {
        this.userDetails = response;
        this.startImage();
      },
      error: (error) => {
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
      },
    });
  }

  logout() {
    this.authService.logout();
    this.userLogged = false; // Atualiza o estado de autenticação após o logout
    this.componentUpdateService.triggerConnectUpdate(); // Dispara a atualização no FeedComponent
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
