import { Component, Input } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ICollegeResponse } from '../../_models/ICollegeResponse';

@Component({
  selector: 'app-feed-college-badge',
  templateUrl: './feed-college-badge.component.html',
  styleUrl: './feed-college-badge.component.scss'
})
export class FeedCollegeBadgeComponent {
  constructor(private sanitizer: DomSanitizer) {}

  @Input() building?: ICollegeResponse;
  buildingImages: SafeUrl[] = [];
  base64: string = 'data:image/png;base64,';

  ngOnChanges() {
    this.startImage();
  }

  startImage() {
    if (this.building?.images) {
      let images = this.building.images;
      images.forEach((image) => {
        this.buildingImages.push(
          this.sanitizer.bypassSecurityTrustUrl(this.base64 + image.imageData64)
        );
      });
    }
  }
}
