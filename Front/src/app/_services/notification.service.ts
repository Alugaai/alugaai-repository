import { IAcceptConnection } from './../_models/IAcceptConnection';
import { map } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { IStudentsWhoInvitationsConnections } from '../_models/IStudentsWhoInvitationsConnections';
import { INotification } from '../_models/INotification';

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

  getNotificationWhoStudentSendConnection(userWhoSendConnection: string) {
    return this.http
      .get<INotification>(
        `${this.baseUrl}notifications/${userWhoSendConnection}`
      )
      .pipe(map((response) => response));
  }

  acceptNotification(
    notificationId: number,
    acceptConnection: IAcceptConnection
  ) {
    return this.http
      .post<any>(`${this.baseUrl}students/${notificationId}`, acceptConnection)
      .pipe(
        map((response) => {
          console.log(response.message);
          return response;
        })
      );
  }
}
