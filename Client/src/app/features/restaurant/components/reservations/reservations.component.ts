import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {RestaurantService} from "../../services/restaurant.service";
import {UserService} from "../../../auth/services/user.service";

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrl: './reservations.component.css'
})
export class ReservationsComponent implements OnInit {
  reservations: Array<any> = [];

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private restaurantService: RestaurantService) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(res => {
      this.restaurantService
        .getReservationsByRestaurantId(res['id'])
        .subscribe((res: any) => {
          this.reservations = res.data;

          for (let reservation of this.reservations) {
            this.userService.getUser(reservation.clientId).subscribe(res => reservation.client = res);
          }
        });
    });
  }
}
