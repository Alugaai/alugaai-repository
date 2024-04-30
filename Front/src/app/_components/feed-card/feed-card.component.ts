import { ChangeDetectorRef, Component, Input } from '@angular/core';
import { IStudent } from '../../_models/IStudent';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { StudentService } from '../../_services/student.service';
import { NotificationService } from '../../_services/notification.service';
import { ComponentUpdateService } from '../../_services/component-update.service';

@Component({
  selector: 'app-feed-card',
  templateUrl: './feed-card.component.html',
  styleUrl: './feed-card.component.scss',
})
export class FeedCardComponent {
  @Input() student?: IStudent;
  constructor(
    private sanitizer: DomSanitizer,
    private studentService: StudentService,
    private componentUpdate: ComponentUpdateService
  ) {}

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
    this.studentService.connect(this.student!.id).subscribe({
      next: (response) => {
        console.log(response);
        this.componentUpdate.triggerConnectUpdate(); // Dispara a atualização no FeedComponent
      },
    });
  }
}
