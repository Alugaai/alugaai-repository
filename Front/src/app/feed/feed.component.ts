import { NotificationService } from './../_services/notification.service';
import { IFilterStudent } from './../_models/IFilterStudent';
import {
  AfterViewInit,
  Component,
  DoCheck,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FeedBadgeClickedComponent } from '../_components/feed-badge-clicked/feed-badge-clicked.component';
import { PropertyService } from '../_services/property.service';
import { IResponseProperty } from '../_models/IResponseProperty';
import { CollegeService } from '../_services/college.service';
import { ICollegeResponse } from '../_models/ICollegeResponse';
import { StudentService } from '../_services/student.service';
import { IPagination } from '../_models/IPagination';
import { IStudent } from '../_models/IStudent';
import { ILocationFilterCity } from '../_models/ILocationFilterCity';
import { IAges } from '../_components/range-slider-filter/range-slider-filter.component';
import { IStudentsWhoInvitationsConnections } from '../_models/IStudentsWhoInvitationsConnections';
import { Subscription } from 'rxjs';
import { ComponentUpdateService } from '../_services/component-update.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.scss'],
})
export class FeedComponent implements AfterViewInit, OnInit, OnDestroy {
  markersProperty: IResponseProperty[] = [];
  markersCollege: ICollegeResponse[] = [];
  studentsWhoInvitationsConnections: Array<IStudentsWhoInvitationsConnections> =
    [];

  lat = -23.4709;
  lng = -47.4851;

  pageSize: number = 1;
  pagination?: IPagination;

  students: Array<IStudent> = [];

  filter: IFilterStudent = {
    name: '',
    initialAge: 0,
    finalAge: 99,
    ownCollege: false,
    interests: [''],
    pageNumber: 1,
    pageSize: this.pageSize,
  };

  userLogged: boolean = false;

  private componentUpdateSubscription: Subscription | undefined;
  private userTokenSubscription: Subscription | undefined;

  constructor(
    private propertyService: PropertyService,
    private dialog: MatDialog,
    private collegeService: CollegeService,
    private studentService: StudentService,
    private notificationService: NotificationService,
    private componentUpdateService: ComponentUpdateService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.userTokenSubscription = this.authService.userLoggedToken$.subscribe(userToken => {
      this.userLogged = !!userToken;
      if (userToken) {
        this.getMyInvitationsForConnections();
      }
  });
  this.componentUpdateSubscription = this.componentUpdateService.updateComponent$.subscribe(() => {
    this.filterStudent(); // Atualize a lista de estudantes
  });

  }

  ngOnDestroy(): void {
    if (this.componentUpdateSubscription) {
      this.componentUpdateSubscription.unsubscribe();
    }
    if (this.userTokenSubscription) {
      this.userTokenSubscription.unsubscribe();
    }
  }


  title = 'angular-gmap';
  @ViewChild('gmapContainer', { static: false }) mapContainer?: ElementRef;
  map?: google.maps.Map;

  coordinates = new google.maps.LatLng(this.lat, this.lng);

  mapOptions: google.maps.MapOptions = {
    center: this.coordinates,
    zoom: 12,
    panControl: false,
    disableDefaultUI: true,
    zoomControl: false,
    scaleControl: false,
    mapTypeControl: false,
    minZoom: 8,
    scrollwheel: true,
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
    this.filterStudent();
    console.log(this.students);
  }

  // filtrar pedidos de conexão
  // pega todos os pedidos de conexão que o usuário logado recebeu, para que eu possa não mostrar ele mesmo na lista de estudantes
  getMyInvitationsForConnections() {
    this.notificationService.getStudentsWhoInvitationsConnections().subscribe({
      next: (response) => {
        this.studentsWhoInvitationsConnections = response.result;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
  isStudentInInvitationsConnections(studentId: string): boolean {
    return this.studentsWhoInvitationsConnections.some(
      (connection) => connection.id === studentId
    );
  }
  ///

  mapInitializer() {
    this.map = new google.maps.Map(
      this.mapContainer?.nativeElement,
      this.mapOptions
    );
    this.filterProperty();
    this.getColleges();
  }

  // Função para filtrar a localização inicial
  onInitialLocationFilter(location: ILocationFilterCity) {
    if (location.lat) {
      this.lat = location.lat;
    }
    if (location.long) {
      this.lng = location.long;
    }
  }

  // Função para filtrar a idade
  onAgeChangeFilter(ages: IAges) {
    if (ages.initialAge) {
      this.filter.initialAge = ages.initialAge;
    }
    if (ages.finalAge) {
      this.filter.finalAge = ages.finalAge;
    }
  }

  // Função para filtrar as proriedades
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
  }

  markerClickHandler(property: IResponseProperty | ICollegeResponse) {
    this.dialog.open(FeedBadgeClickedComponent, {
      data: property,
      width: '500px',
    });
  }

  filterStudent() {
    this.coordinates = new google.maps.LatLng(this.lat, this.lng);
    this.mapOptions.center = this.coordinates;
    this.mapInitializer();
    this.studentService.filterStudent(this.filter).subscribe({
      next: (response) => {
        if (response.result && response.pagination) {
          this.students = response.result;
          this.pagination = response.pagination;
        }
      },
    });
  }

  onInteressesChange(interesses: string[]) {
    this.filter.interests = interesses;
  }

  pageChanged(event: any) {
    if (this.filter.pageNumber != event.page) {
      this.filter.pageNumber = event.page;
      this.filterStudent();
    }
  }
}
