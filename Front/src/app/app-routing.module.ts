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


const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'autocomplete',
    component: AutoCompleteComponent,
  },

  {
    path: 'card',
    component: FeedCardComponent,
  },
  {
    path: 'faculdades',
    component: FeedComponent,
  },
  {
    path: 'universitarios',
    component: HomeComponent,
  },
  {
    path: 'destaque-se',
    component: HomeComponent,
  },
  {
    path: 'anuncie',
    component: HomeComponent,
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
    component: StudentCompleteProfileComponent,
    canActivate: [authGuard],
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
