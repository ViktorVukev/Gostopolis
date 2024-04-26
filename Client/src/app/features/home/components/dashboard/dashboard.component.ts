import { Component, OnInit } from '@angular/core';
import { RestaurantService } from '../../../restaurant/services/restaurant.service';
import { AccommodationService } from '../../../accommodation/services/accommodation.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  restaurants!: Array<any>;
  accommodations!: Array<any>;

  constructor(
    private restaurantService: RestaurantService,
    private accommodationService: AccommodationService) { }

  ngOnInit(): void {
    this.restaurantService
      .mine()
      .subscribe((res: any) => this.restaurants = res.data);

    this.accommodationService
      .mine()
      .subscribe(res => this.accommodations = res);
  }
}
