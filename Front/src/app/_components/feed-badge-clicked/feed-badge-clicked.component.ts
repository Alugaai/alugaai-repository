import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IResponseProperty } from '../../_models/IResponseProperty';
import { ICollegeResponse } from '../../_models/ICollegeResponse';
import { PropertyService } from '../../_services/property.service';
import { IFindPropertyDetailsById } from '../../_models/IFindPropertyDetailsById';

@Component({
  selector: 'app-feed-badge-clicked',
  templateUrl: './feed-badge-clicked.component.html',
  styleUrl: './feed-badge-clicked.component.scss',
})
export class FeedBadgeClickedComponent implements OnInit{
  constructor(@Inject(MAT_DIALOG_DATA) public building: any, private propertyService: PropertyService) {}
  propertyForDetails?: IFindPropertyDetailsById;

  ngOnInit(): void {
    this.verifyIsCollege();
    this.verifyIsProperty();
    this.propertyDetails();
  }

  isProperty: boolean = false;


  verifyIsProperty() {
    if ('price' in this.building) {
      this.isProperty = true;
    }
    this.building = this.building as IResponseProperty;
  }

  verifyIsCollege() {
    if ('address' in this.building) {
      this.isProperty = false;
    }
    this.building = this.building as ICollegeResponse;
  }

  propertyDetails() {
    if (this.isProperty) {
      this.propertyService.findPropertyDetailsById(this.building.id).subscribe((response) => {
        console.log(response);
        this.propertyForDetails = response;
      });
    }
  }


}
