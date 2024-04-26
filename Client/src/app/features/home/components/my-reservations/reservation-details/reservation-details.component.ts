import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-reservation-details',
  templateUrl: './reservation-details.component.html',
  styleUrl: './reservation-details.component.css'
})
export class ReservationDetailsComponent implements OnInit {
  reservation: any;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      if (window.history.state && window.history.state.data) {
        this.reservation = window.history.state.data;

        console.log(this.reservation);
      }
    });
  }

  openPrintDialog(): void {
    window.print();
  }
}
