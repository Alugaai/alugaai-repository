import { NgModule } from '@angular/core';
import {
  BrowserModule,
  provideClientHydration,
} from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { HomeComponent } from './home/home.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { FeedCardComponent } from './_components/feed-card/feed-card.component';
import { MatChipsModule } from '@angular/material/chips';
import { MainRegisterComponent } from './register/main-register/main-register.component';
import { AnuncianteRegisterComponent } from './register/anunciante-register/anunciante-register.component';
import { UniversitarioRegisterComponent } from './register/universitario-register/universitario-register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { JwtInterceptor } from './_interceptor/jwt.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ErrorInterceptor } from './_interceptor/error.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { FeedComponent } from './feed/feed.component';
import { GoogleMapsModule } from '@angular/google-maps';
import { FeedBadgeClickedComponent } from './_components/feed-badge-clicked/feed-badge-clicked.component';
import { FeedPropertyBadgeComponent } from './_components/feed-property-badge/feed-property-badge.component';
import { FeedCollegeBadgeComponent } from './_components/feed-college-badge/feed-college-badge.component';
import { LoadingInterceptor } from './_interceptor/loading.interceptor';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import { AutoCompleteComponent } from './_components/auto-complete/auto-complete.component';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { PlaceAutocompleteComponent } from './_components/place-autocomplete/place-autocomplete.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { StudentCompleteProfileComponent } from './complete-profile/student-complete-profile/student-complete-profile.component';
import {MatBadgeModule} from '@angular/material/badge';
import {MatMenuModule} from '@angular/material/menu';
import { NotificationsCardComponent } from './_components/notifications-card/notifications-card.component';
import {MatSliderModule} from '@angular/material/slider';
import { RangeSliderFilterComponent } from './_components/range-slider-filter/range-slider-filter.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    FeedCardComponent,
    MainRegisterComponent,
    AnuncianteRegisterComponent,
    UniversitarioRegisterComponent,
    LoginComponent,
    FeedComponent,
    FeedBadgeClickedComponent,
    FeedPropertyBadgeComponent,
    FeedCollegeBadgeComponent,
    AutoCompleteComponent,
    PlaceAutocompleteComponent,
    StudentCompleteProfileComponent,
    NotificationsCardComponent,
    RangeSliderFilterComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatChipsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    NgxSpinnerModule,
    ModalModule.forRoot(),
    BrowserAnimationsModule,
    PaginationModule.forRoot(),
    GoogleMapsModule,
    NgxSpinnerModule,
    CollapseModule.forRoot(),
    NgxMaskDirective,
    NgxMaskPipe,
    MatAutocompleteModule,
    MatInputModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatBadgeModule,
    MatMenuModule,
    MatSliderModule,
  ],
  providers: [
    provideClientHydration(),
    provideAnimationsAsync('noop'),
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    provideAnimationsAsync(),
    provideNgxMask()
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

