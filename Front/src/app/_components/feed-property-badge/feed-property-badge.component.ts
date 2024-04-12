import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { IFindPropertyDetailsById } from '../../_models/IFindPropertyDetailsById';

@Component({
  selector: 'app-feed-property-badge',
  templateUrl: './feed-property-badge.component.html',
  styleUrls: ['./feed-property-badge.component.scss']
})
export class FeedPropertyBadgeComponent implements OnChanges {
  @Input() building?: IFindPropertyDetailsById;
  buildingImages?: string[];

  ngOnChanges() {
    this.buildingImages = this.building?.images.images;
    console.log(this.buildingImages);
  }
}
