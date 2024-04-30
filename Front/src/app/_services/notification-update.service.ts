import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationUpdateService {
  private notificationUpdate = new Subject<void>();

  notificationUpdate$ = this.notificationUpdate.asObservable();

  triggerConnectUpdate() {
    this.notificationUpdate.next();
  }
}
