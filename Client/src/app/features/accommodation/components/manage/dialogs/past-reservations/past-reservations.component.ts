import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from "@angular/material/dialog";

@Component({
  selector: 'app-past-reservations',
  templateUrl: './past-reservations.component.html',
  styleUrl: './past-reservations.component.css'
})
export class PastReservationsComponent {
  reservations: any;

  constructor(@Inject(MAT_DIALOG_DATA) private data: any) {
    this.reservations = data;

    for (let reservation of this.reservations) {
      //reservation.tableNumbers = reservation.tables.map((t: any) => t.number).join(', ');
    }
  }
}
