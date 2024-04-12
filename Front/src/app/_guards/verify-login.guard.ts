import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { map } from 'rxjs';
import { AuthService } from '../_services/auth.service';

export const verifyLoginGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.userLoggedToken$.pipe(
    map((user) => {
      if (user) {
        router.navigate(['home']);
        return false;
      } else {
        return true;
      }
    })
  );
};
