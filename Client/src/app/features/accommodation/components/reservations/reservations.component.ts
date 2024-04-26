import {Component, OnInit} from '@angular/core';
import {AccommodationService} from "../../services/accommodation.service";
import {ImageItem} from "ng-gallery";
import {ActivatedRoute} from "@angular/router";
import {UserService} from "../../../auth/services/user.service";

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrl: './reservations.component.css'
})
export class ReservationsComponent implements OnInit {
  displayedColumns: string[] = ['startDate', 'endDate', 'guests', 'name', 'dateOfBirth'];
  reservations: Array<any> = [];
  user: any;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private accommodationService: AccommodationService) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(res => {
      this.accommodationService
        .getReservationsByAccommodationId(res['id'])
        .subscribe(reservations => {
          this.reservations = reservations;

          for (let reservation of reservations) {
            this.userService.getUser(reservation.clientId).subscribe(res => reservation.client = res);
          }
        });
    });
  }
}
