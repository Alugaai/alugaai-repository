import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IResponseProperty } from '../../_models/IResponseProperty';

@Component({
  selector: 'app-feed-badge-clicked',
  templateUrl: './feed-badge-clicked.component.html',
  styleUrl: './feed-badge-clicked.component.scss',
})
export class FeedBadgeClickedComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public property: IResponseProperty) {}
}
