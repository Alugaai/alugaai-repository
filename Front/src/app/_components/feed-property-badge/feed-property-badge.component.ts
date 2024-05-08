import { Component, Input, OnChanges } from '@angular/core';
import { IFindPropertyDetailsById } from '../../_models/IFindPropertyDetailsById';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

interface Image {
  src: string;
  alt: string;
  index: number;
}

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
  //image carousel related------
  images = [
    { src: '../../../assets/images/imageProperty1.png', alt: 'Image 1', index: 0 },
    { src: '../../../assets/images/imageProperty2.png', alt: 'Image 2', index: 1 },
    { src: '../../../assets/images/imageProperty3.png', alt: 'Image 2', index: 2 },
    { src: '../../../assets/images/imageProperty4.png', alt: 'Image 2', index: 3 },
  ];
  currentImageIndex: number = 0;

 

  next() {
    this.currentImageIndex = (this.currentImageIndex + 2) % this.images.length;
  }

  prev() {
    this.currentImageIndex = (this.currentImageIndex - 2 + this.images.length) % this.images.length;
  }

  visibleImages(): Image[] {
    const index1 = this.currentImageIndex % this.images.length;
    const index2 = (this.currentImageIndex + 1) % this.images.length;
    return [this.images[index1], this.images[index2]];
  }

  //image carousel related--------

  startImage() {
    if (this.building?.owner?.image) {
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
