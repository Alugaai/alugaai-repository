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
import { MatIconModule } from '@angular/material/icon';
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
import { FeedFilterComponent } from './_components/feed-filter/feed-filter.component';
import { GoogleMapsModule } from '@angular/google-maps';
import { FeedBadgeClickedComponent } from './_components/feed-badge-clicked/feed-badge-clicked.component';
import { FeedPropertyBadgeComponent } from './_components/feed-property-badge/feed-property-badge.component';

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
    FeedFilterComponent,
    FeedBadgeClickedComponent,
    FeedPropertyBadgeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatIconModule,
    MatChipsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    NgxSpinnerModule,
    ModalModule.forRoot(),
    BrowserAnimationsModule,
    PaginationModule.forRoot(),
    GoogleMapsModule
  ],
  providers: [
    provideClientHydration(),
    provideAnimationsAsync('noop'),
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    provideAnimationsAsync(),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
