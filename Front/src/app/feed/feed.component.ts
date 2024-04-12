import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FeedBadgeClickedComponent } from '../_components/feed-badge-clicked/feed-badge-clicked.component';
import { PropertyService } from '../_services/property.service';
import { IResponseProperty } from '../_models/IResponseProperty';
import { CollegeService } from '../_services/college.service';
import { ICollegeResponse } from '../_models/ICollegeResponse';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.scss'],
})
export class FeedComponent implements AfterViewInit {
  markersProperty: IResponseProperty[] = [];
  markersCollege: ICollegeResponse[] = [];

  constructor(
    private propertyService: PropertyService,
    private dialog: MatDialog,
    private collegeService: CollegeService
  ) {}

  title = 'angular-gmap';
  @ViewChild('gmapContainer', { static: false }) mapContainer?: ElementRef;
  map?: google.maps.Map;
  lat = -23.4709;
  lng = -47.4851;

  coordinates = new google.maps.LatLng(this.lat, this.lng);

  mapOptions: google.maps.MapOptions = {
    center: this.coordinates,
    zoom: 12,
    panControl: false,
    disableDefaultUI: true,
    zoomControl: false,
    scaleControl: false,
    mapTypeControl: false,
    styles: [
      {
        elementType: 'geometry',
        stylers: [
          {
            color: '#f5f5f5',
          },
        ],
      },
      {
        elementType: 'labels.icon',
        stylers: [
          {
            visibility: 'off',
          },
        ],
      },
      {
        elementType: 'labels.text.fill',
        stylers: [
          {
            color: '#616161',
          },
        ],
      },
      {
        elementType: 'labels.text.stroke',
        stylers: [
          {
            color: '#f5f5f5',
          },
        ],
      },
      {
        featureType: 'administrative.land_parcel',
        elementType: 'labels',
        stylers: [
          {
            visibility: 'off',
          },
        ],
      },
      {
        featureType: 'administrative.land_parcel',
        elementType: 'labels.text.fill',
        stylers: [
          {
            color: '#bdbdbd',
          },
        ],
      },
      {
        featureType: 'poi',
        elementType: 'geometry',
        stylers: [
          {
            color: '#eeeeee',
          },
        ],
      },
      {
        featureType: 'poi',
        elementType: 'labels.text',
        stylers: [
          {
            visibility: 'off',
          },
        ],
      },
      {
        featureType: 'poi',
        elementType: 'labels.text.fill',
        stylers: [
          {
            color: '#757575',
          },
        ],
      },
      {
        featureType: 'poi.park',
        elementType: 'geometry',
        stylers: [
          {
            color: '#e5e5e5',
          },
        ],
      },
      {
        featureType: 'poi.park',
        elementType: 'labels.text.fill',
        stylers: [
          {
            color: '#9e9e9e',
          },
        ],
      },
      {
        featureType: 'road',
        elementType: 'geometry',
        stylers: [
          {
            color: '#ffffff',
          },
        ],
      },
      {
        featureType: 'road.arterial',
        elementType: 'labels.text.fill',
        stylers: [
          {
            color: '#757575',
          },
        ],
      },
      {
        featureType: 'road.highway',
        elementType: 'geometry',
        stylers: [
          {
            color: '#dadada',
          },
        ],
      },
      {
        featureType: 'road.highway',
        elementType: 'labels.text.fill',
        stylers: [
          {
            color: '#616161',
          },
        ],
      },
      {
        featureType: 'road.local',
        elementType: 'labels',
        stylers: [
          {
            visibility: 'off',
          },
        ],
      },
      {
        featureType: 'road.local',
        elementType: 'labels.text.fill',
        stylers: [
          {
            color: '#9e9e9e',
          },
        ],
      },
      {
        featureType: 'transit.line',
        elementType: 'geometry',
        stylers: [
          {
            color: '#e5e5e5',
          },
        ],
      },
      {
        featureType: 'transit.station',
        elementType: 'geometry',
        stylers: [
          {
            color: '#eeeeee',
          },
        ],
      },
      {
        featureType: 'water',
        elementType: 'geometry',
        stylers: [
          {
            color: '#c9c9c9',
          },
        ],
      },
      {
        featureType: 'water',
        elementType: 'labels.text.fill',
        stylers: [
          {
            color: '#9e9e9e',
          },
        ],
      },
    ],
  };

  ngAfterViewInit() {
    this.mapInitializer();
  }

  mapInitializer() {
    this.map = new google.maps.Map(
      this.mapContainer?.nativeElement,
      this.mapOptions
    );
    this.filterProperty();
    this.getColleges();
  }

  filterProperty() {
    this.propertyService.filterProperty().subscribe({
      next: (response) => {
        if (response.result) {
          this.markersProperty = response.result;
          this.markersProperty.forEach((property) => {
            if (!property.options) {
              property.options = {} as google.maps.MarkerOptions;
            }
            property.options.label = {
              text: 'R$' + property.price.toString() + ',00',
              color: '#FFFFFF',
              fontFamily: 'Inter',
              fontWeight: 'bold',
            };
            property.options.icon = '../../assets/images/iconProperty.svg';

            // Criar um novo marcador para cada propriedade
            const newMarker = new google.maps.Marker({
              position: {
                lat: property.position.lat,
                lng: property.position.lng,
              },
              map: this.map,
              title: property.id.toString(),
              icon: property.options.icon,
              label: property.options.label,
            });

            // Adicionar um listener de clique para abrir o modal
            newMarker.addListener('click', () => {
              this.markerClickHandler(property);
            });
          });
        }
      },
    });
  }

  getColleges() {
    this.collegeService.getColleges().subscribe({
      next: (response) => {
        if (response) {
          this.markersCollege = response;
          this.markersCollege.forEach((college) => {
            if (!college.options) {
              college.options = {} as google.maps.MarkerOptions;
            }
            college.options.label = {
              text: college.name,
              color: '#FFFFFF',
              fontFamily: 'Inter',
              fontWeight: 'bold',
            };
            college.options.icon = '../../assets/images/iconCollege.svg';

            // Criar um novo marcador para cada propriedade
            const newMarker = new google.maps.Marker({
              position: {
                lat: college.position.lat,
                lng: college.position.lng,
              },
              map: this.map,
              title: college.id.toString(),
              icon: college.options.icon,
              label: college.options.label,
            });

            // Adicionar um listener de clique para abrir o modal
            newMarker.addListener('click', () => {
              this.markerClickHandler(college);
            });
          });
        }
      },
    });
  };






  markerClickHandler(property: IResponseProperty | ICollegeResponse) {
    this.dialog.open(FeedBadgeClickedComponent, {
      data: property,
      width: '500px',
    });
  }

}
function getColleges() {
  throw new Error('Function not implemented.');
}

