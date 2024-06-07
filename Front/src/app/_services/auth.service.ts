import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUserToken } from '../_models/IUserToken';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { ILogin } from '../_models/ILogin';
import { IRegister } from '../_models/IRegister';
import { IUserDetails } from '../_models/IUserDetails';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl: string = environment.apiUrl;

  private userLogged = new BehaviorSubject<IUserToken | null>(null);
  userLoggedToken$ = this.userLogged.asObservable();

  constructor(private http: HttpClient) {}

  setUserLogged(userToken: IUserToken) {
    this.userLogged.next(userToken);
  }

  logout() {
    this.userLogged.next(null);
    localStorage.clear();
  }

  login(loginBody: ILogin) {
    return this.http.post<any>(this.baseUrl + 'auth/login', loginBody).pipe(
      map((response: IUserToken) => {
        if (response) {
          localStorage.setItem('user', JSON.stringify(response));
          this.setUserLogged(response);
        }
        return response;
      })
    );
  }

  registerStudent(user: IRegister) {
    return this.http
      .post<any>(this.baseUrl + 'auth/register/student', user)
      .pipe(
        map((response) => {
          return response;
        })
      );
  }

  registerOwner(user: IRegister) {
    return this.http.post<any>(this.baseUrl + 'auth/register/owner', user).pipe(
      map((response) => {
        return response;
      })
    );
  }

  userDetailsByEmail(email: string) {
    return this.http.get<any>(this.baseUrl + `user/details/${email}`).pipe(
      map((response: IUserDetails) => {
        return response;
      })
    );
  }

  userDetailsById() {
    return this.http.get(`http://localhost:8002/user/details/detailsById`).pipe(
      map((response) => {
        console.log(response, 'userDetails');
      })
    );
  }
}
