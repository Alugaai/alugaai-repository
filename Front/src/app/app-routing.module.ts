import { verifyLoginGuard } from './_guards/verify-login.guard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { FeedCardComponent } from './_components/feed-card/feed-card.component';
import { MainRegisterComponent } from './register/main-register/main-register.component';
import { UniversitarioRegisterComponent } from './register/universitario-register/universitario-register.component';
import { AnuncianteRegisterComponent } from './register/anunciante-register/anunciante-register.component';
import { LoginComponent } from './login/login.component';
import { FeedComponent } from './feed/feed.component';
import { AutoCompleteComponent } from './_components/auto-complete/auto-complete.component';
import { StudentCompleteProfileComponent } from './complete-profile/student-complete-profile/student-complete-profile.component';
import { authGuard } from './_guards/auth.guard';
import { NotificationsCardComponent } from './_components/notifications-card/notifications-card.component';
import { RangeSliderFilterComponent } from './_components/range-slider-filter/range-slider-filter.component';
import { PriceFilterComponent } from './_components/price-filter/price-filter.component';
import { FeedPropertyBadgeComponent } from './_components/feed-property-badge/feed-property-badge.component';
import { PropertyPageComponent } from './_components/property-page/property-page.component';



const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent, 
  },
 
  {
    path: 'faculdades',
    component: FeedComponent,
  },
  {
    path: 'entrar',
    component: LoginComponent,
    canActivate: [verifyLoginGuard],
  },
  {
    path: 'registrar',
    component: MainRegisterComponent,
  },
  {
    path: 'registrar/universitario',
    component: UniversitarioRegisterComponent,
  },
  {
    path: 'registrar/anunciante',
    component: AnuncianteRegisterComponent,
  },
  {
    path: 'perfil/estudante',
    component: StudentCompleteProfileComponent
  },
  {
    path: 'pricefilter',
    component: PriceFilterComponent
  },
  {
    path: 'propertypage',
    component: PropertyPageComponent
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
