import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';
import { AuthService } from '../_services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const toastr = inject(ToastrService);
  const router = inject(Router);

  return authService.userLoggedToken$.pipe(
    map((user) => {
      if (user) {
        return true;
      } else {
        toastr.error('Você não tem permissão para acessar esta página');
        router.navigate(['/entrar']);
        return false;
      }
    })
  );
};
