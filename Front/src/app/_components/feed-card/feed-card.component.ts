import { Component, Input } from '@angular/core';
import { IStudent } from '../../_models/IStudent';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-feed-card',
  templateUrl: './feed-card.component.html',
  styleUrl: './feed-card.component.scss'
})
export class FeedCardComponent {
  @Input() student?: IStudent;
  constructor(private sanitizer: DomSanitizer) {}

  userImage: SafeUrl = '';
  base64: string = 'data:image/png;base64,';

  ngOnChanges() {
    this.startImage();
  }

  startImage() {
    if (this.student?.image) {
      let image = this.student.image;
      this.userImage = this.sanitizer.bypassSecurityTrustUrl(this.base64 + image.imageData64)
    }
  }

}
