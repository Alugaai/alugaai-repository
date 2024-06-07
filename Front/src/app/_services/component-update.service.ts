import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ComponentUpdateService {
  private updateComponent = new Subject<void>();

  updateComponent$ = this.updateComponent.asObservable();

  triggerConnectUpdate() {
    this.updateComponent.next();
  }
}
