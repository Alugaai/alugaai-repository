import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { IStudent } from '../../_models/IStudent';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { StudentService } from '../../_services/student.service';
import { NotificationService } from '../../_services/notification.service';
import { ComponentUpdateService } from '../../_services/component-update.service';
import { AuthService } from '../../_services/auth.service';

@Component({
  selector: 'app-feed-card',
  templateUrl: './feed-card.component.html',
  styleUrl: './feed-card.component.scss',
})
export class FeedCardComponent implements OnInit{
  @Input() student?: IStudent;
  userLogged: boolean = false;
  constructor(
    private sanitizer: DomSanitizer,
    private studentService: StudentService,
    private componentUpdate: ComponentUpdateService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.authService.userLoggedToken$.subscribe((userToken) => {
      this.userLogged = !!userToken;
    });
  }

  userImage: SafeUrl = '';
  base64: string = 'data:image/png;base64,';

  ngOnChanges() {
    this.startImage();
  }

  startImage() {
    if (this.student?.image) {
      let image = this.student.image;
      this.userImage = this.sanitizer.bypassSecurityTrustUrl(
        this.base64 + image.imageData64
      );
    }
  }

  connect() {
    if (this.userLogged) {
      this.studentService.connect(this.student!.id).subscribe({
        next: (response) => {
          this.componentUpdate.triggerConnectUpdate(); // Dispara a atualização no FeedComponent
        },
      });
    }
  }
}
