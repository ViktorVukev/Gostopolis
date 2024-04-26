import {Component, OnInit} from '@angular/core';
import { Constants } from '../../../../shared/interfaces/Constants';
import {ActivatedRoute, Router} from "@angular/router";
import {RestaurantService} from "../../../restaurant/services/restaurant.service";
import {DatePipe} from "@angular/common";
import {PropertyType} from "../../../../shared/interfaces/PropertyType";
import {MatCheckboxChange} from "@angular/material/checkbox";

@Component({
  selector: 'app-search-restaurant',
  templateUrl: './search-restaurant.component.html',
  styleUrl: './search-restaurant.component.css'
})
export class SearchRestaurantComponent implements OnInit {
  data: any;
  restaurants: any;
  filters: any;
  types: Array<number> = [];
  stars: Array<number> = [];
  restaurantTypes: Array<PropertyType> = [];
  displayedRestaurantTypesList: Array<PropertyType> = [];
  timeOptions: Array<string> = Constants.TimeOptions;
  currentDate = new Date();
  valueRestaurantGuests: number = 1;
  valueTables: number = 1;
  showMoreTypes = false;

  constructor(
    private route: ActivatedRoute,
    private restaurantService: RestaurantService,
    private router: Router,
    private datePipe: DatePipe) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(res => {
      this.data = {
        town: res['town'],
        startDate: this.datePipe.transform(res['startDate'], 'yyyy-MM-dd'),
        startTime: res['startTime'],
        tables: res['tables'],
        people: res['people'],
      };

      this.restaurantService
        .listing({
          town: res['town'],
          startTime: this.datePipe.transform(res['startDate'], 'yyyy-MM-dd') + 'T' + res['startTime'],
          tables: res['tables'],
          people: res['people'],
        })
        .subscribe(res => {
          this.restaurants = res.data;

          this.filters = {
            hasParking: false,
            hasPosTerminal: false,
            acceptPets: false,
            stars: null,
            types: null
          }
        });
    });

    this.restaurantService.getRestaurantTypes().subscribe(res => {
      this.restaurantTypes = res.data;

      this.displayedRestaurantTypesList = this.restaurantTypes.slice(0, 3);
    });
  }

  search(): void {
    this.restaurantService
      .listing({
        town: this.data.town,
        tables: this.data.tables,
        people: this.data.people,
        startTime: this.datePipe.transform(this.data.startDate, 'yyyy-MM-dd') + 'T' + this.data.startTime
      })
      .subscribe(res => {
        this.restaurants = res.data;
      });
  }

  details(id: number): void {
    let data = {
      ...this.data,
      id: id,
    }

    this.router.navigate(['/restaurant/details'], { queryParams: data });
  }

  typeChange(event: MatCheckboxChange, type: number): void {
    if (event.checked) {
      this.types.push(type);
    } else {
      let index = this.types.indexOf(type);
      if (index >= 0) {
        this.types.splice(index, 1);
      }
    }
  }

  starsChange(event: MatCheckboxChange, stars: number): void {
    if (event.checked) {
      this.stars.push(stars);
    } else {
      let index = this.stars.indexOf(stars);
      if (index >= 0) {
        this.stars.splice(index, 1);
      }
    }
  }

  decrementRestaurantGuests() {
    if (this.data.people > 1) {
      this.data.people--;
    }
  }

  incrementRestaurantGuests() {
    this.data.people++;
  }

  decrementTables() {
    if (this.data.tables > 1) {
      this.data.tables--;
    }
  }

  incrementTables() {
    this.data.tables++;
  }

   //See more types button
   toggleShowMoreTypes(): void {
    this.showMoreTypes = !this.showMoreTypes;
    if (this.showMoreTypes) {
      this.displayedRestaurantTypesList = this.restaurantTypes;
    } else {
      this.displayedRestaurantTypesList = this.restaurantTypes.slice(0, 3);
    }
  }

  filter(): void {
    this.restaurantService
      .filter({
        town: this.data.town,
        startTime: this.datePipe.transform(this.data.startDate, 'yyyy-MM-dd') + 'T' + this.data.startTime,
        tables: this.data.tables,
        people: this.data.people,
        hasParking: this.filters.hasParking,
        hasPosTerminal: this.filters.hasPosTerminal,
        acceptPets: this.filters.acceptPets,
        stars: this.stars.join(' ') || '1 2 3 4 5',
        types: this.types.join(' ') || this.restaurantTypes.map(type => type.id).join(' ')
      })
      .subscribe(res => {
        this.restaurants = res.data;
      });
  }

  sort(event: any): void {
    switch (event) {
      case 'nameDescending':
        this.restaurants = this.restaurants.sort((a: any, b: any) => a.name > b.name ? -1 : (a.name > b.name) ? 1 : 0)
        break;
      case 'nameAscending':
        this.restaurants = this.restaurants.sort((a: any, b: any) => a.name < b.name ? -1 : (a.name > b.name) ? 1 : 0)
        break;
    }
  }
}
