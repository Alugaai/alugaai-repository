import { NotificationService } from './../../_services/notification.service';
import {
  Component,
  Input,
  DoCheck,
  OnChanges,
  OnInit,
  Output,
  ChangeDetectorRef,
} from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { IStudentsWhoInvitationsConnections } from '../../_models/IStudentsWhoInvitationsConnections';
import { INotification } from '../../_models/INotification';
import { ToastrService } from 'ngx-toastr';
import { EventEmitter } from 'stream';
import { ComponentUpdateService } from '../../_services/component-update.service';
import { NotificationUpdateService } from '../../_services/notification-update.service';

@Component({
  selector: 'app-notifications-card',
  templateUrl: './notifications-card.component.html',
  styleUrl: './notifications-card.component.scss',
})
export class NotificationsCardComponent implements OnInit {
  @Input()
  studentsWhoInvitationsConnections?: IStudentsWhoInvitationsConnections;
  notification?: INotification;
  constructor(
    private sanitizer: DomSanitizer,
    private notificationService: NotificationService,
    private notificationUpdateService: NotificationUpdateService,
    private componentUpdateService: ComponentUpdateService
  ) {}
  ngOnInit(): void {
    this.getNotification();
    this.startImage();
  }

  userImage: SafeUrl = '';
  base64: string = 'data:image/png;base64,';

  startImage() {
    if (this.studentsWhoInvitationsConnections?.imageUser) {
      let image = this.studentsWhoInvitationsConnections.imageUser;
      this.userImage = this.sanitizer.bypassSecurityTrustUrl(
        this.base64 + image.imageData64
      );
    }
  }

  getNotification() {
    this.notificationService
      .getNotificationWhoStudentSendConnection(
        this.studentsWhoInvitationsConnections!.id
      )
      .subscribe({
        next: (response) => {
          this.notification = response;
          console.log(this.notification);
        },
      });
  }

  acceptOrRescue(condition: boolean) {
    this.notificationService
      .acceptNotification(this.notification!.id, {
        connectionWhyIHandle: this.notification!.userWhoSend,
        action: condition,
      })
      .subscribe({
        next: (response) => {
          console.log(response);
          // Comunicação com o serviço de atualização de notificações
          this.notificationUpdateService.triggerConnectUpdate();

          // Se a notificação foi recusada, atualize o feed novamente
          if (condition == false) {
            this.componentUpdateService.triggerConnectUpdate();
          }
        },
        error: (error) => {
          console.log(error);
        },
      });
  }

}
