import { NotificationService } from './../../_services/notification.service';
import { Component, Input } from '@angular/core';
import { IStudent } from '../../_models/IStudent';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { IStudentsWhoInvitationsConnections } from '../../_models/IStudentsWhoInvitationsConnections';

@Component({
  selector: 'app-notifications-card',
  templateUrl: './notifications-card.component.html',
  styleUrl: './notifications-card.component.scss',
})
export class NotificationsCardComponent {
  @Input()
  studentsWhoInvitationsConnections?: IStudentsWhoInvitationsConnections;
  constructor(
    private sanitizer: DomSanitizer,
    private notificationService: NotificationService
  ) {}

  userImage: SafeUrl = '';
  base64: string = 'data:image/png;base64,';

  ngOnChanges() {
    this.startImage();
    console.log(this.studentsWhoInvitationsConnections);
  }

  startImage() {
    if (this.studentsWhoInvitationsConnections?.imageUser) {
      let image = this.studentsWhoInvitationsConnections.imageUser;
      this.userImage = this.sanitizer.bypassSecurityTrustUrl(
        this.base64 + image.imageData64
      );
    }
  }

  accept() {
    if (this.studentsWhoInvitationsConnections) {
      const notificationId =
        this.studentsWhoInvitationsConnections.notificationsIds?.[0];
      if (notificationId) {
        this.notificationService
          .acceptConnection(notificationId, {
            connectionWhyIHandle: this.studentsWhoInvitationsConnections.id,
            action: true,
          })
          .subscribe({
            next: (response) => {
              this.studentsWhoInvitationsConnections = undefined;
            },
          });
      }
    }
  }
}
