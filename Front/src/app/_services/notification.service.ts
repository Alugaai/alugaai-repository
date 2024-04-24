import { IAcceptConnection } from './../_models/IAcceptConnection';
import { map } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { IStudentsWhoInvitationsConnections } from '../_models/IStudentsWhoInvitationsConnections';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  constructor(private http: HttpClient) {}

  baseUrl: string = environment.apiUrl;

  getNotifications() {
    return this.http
      .get<number>(`${this.baseUrl}notifications/count`)
      .pipe(map((response) => response));
  }

  getStudentsWhoInvitationsConnections() {
    return this.http
      .get<any>(`${this.baseUrl}myinvitationsforconnection`)
      .pipe(map((response) => response));
  }

  acceptConnection(
    notificationId: number,
    acceptConnection: IAcceptConnection
  ) {
    return this.http
      .post<IAcceptConnection>(`${this.baseUrl}students/${notificationId}`, {
        acceptConnection,
      })
      .pipe(map((response) => {
        console.log(response);
        return response;
      }));
  }
}
