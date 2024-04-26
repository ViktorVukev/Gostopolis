import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {
  private spinnerCounter = new BehaviorSubject<number>(0);
  spinnerCounter$ = this.spinnerCounter.asObservable();

  show(): void {
    this.spinnerCounter.next(this.spinnerCounter.value + 1);
  }

  hide(): void {
    this.spinnerCounter.next(this.spinnerCounter.value - 1);
  }

}
