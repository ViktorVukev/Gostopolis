import {Component, OnInit} from '@angular/core';
import {AccommodationService} from "../../../accommodation/services/accommodation.service";
import {RestaurantService} from "../../../restaurant/services/restaurant.service";
import {PropertyType} from "../../../../shared/interfaces/PropertyType";
import {Router} from "@angular/router";

@Component({
  selector: 'app-my-reservations',
  templateUrl: './my-reservations.component.html',
  styleUrl: './my-reservations.component.css'
})
export class MyReservationsComponent implements OnInit {
  date: Date = new Date;
  accommodationTypes!: Array<PropertyType>;
  restaurantTypes!: Array<PropertyType>;
  accommodationReservations: Array<any> = [];
  restaurantReservations: Array<any> = [];

  constructor(
    private router: Router,
    private accommodationService: AccommodationService,
    private restaurantService: RestaurantService) {
  }

  ngOnInit(): void {
    this.accommodationService.getAccommodationTypes().subscribe(res => {
      this.accommodationTypes = res;

      this.accommodationService
        .getReservations()
        .subscribe((reservations: any) => {
          this.accommodationReservations = reservations;

          for (let reservation of this.accommodationReservations) {
            reservation.accommodation.type = this.accommodationTypes.filter(t => t.id == reservation.accommodation.typeId);
          }

          console.log(this.accommodationReservations);
        });
    });

    this.restaurantService.getRestaurantTypes().subscribe(res => {
      this.restaurantTypes = res.data;

      this.restaurantService
        .getReservations()
        .subscribe((reservations: any) => {
          this.restaurantReservations = reservations.data;

          for (let reservation of this.restaurantReservations) {
            reservation.restaurant.type = this.restaurantTypes.filter(t => t.id == reservation.restaurant.typeId);
          }

          console.log(this.restaurantReservations);
        });
    });
  }

  dateParse(dateString: string): Date {
    return new Date(dateString);
  }

  details(reservation: any): void {
    this.router.navigate(['/reservation'], { state: { data: reservation } });
  }
}
