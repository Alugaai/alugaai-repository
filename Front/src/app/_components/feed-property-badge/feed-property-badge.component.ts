import { Component, Input, OnChanges } from '@angular/core';
import { IFindPropertyDetailsById } from '../../_models/IFindPropertyDetailsById';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-feed-property-badge',
  templateUrl: './feed-property-badge.component.html',
  styleUrls: ['./feed-property-badge.component.scss'],
})
export class FeedPropertyBadgeComponent implements OnChanges {
  constructor(private sanitizer: DomSanitizer) {}

  @Input() building?: IFindPropertyDetailsById;
  buildingImages: SafeUrl[] = [];
  ownerImage: SafeUrl = '';
  base64: string = 'data:image/png;base64,';

  ngOnChanges() {
    this.startImage();
  }

  startImage() {
    console.log(this.building);
    if (this.building?.owner.image) {
      let image = this.building.owner.image.imageData64;
      this.ownerImage = this.sanitizer.bypassSecurityTrustUrl(
        this.base64 + image
      );
    }
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
