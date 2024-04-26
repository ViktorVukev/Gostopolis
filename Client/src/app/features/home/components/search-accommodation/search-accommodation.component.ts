import {Component, OnInit, ViewChild} from '@angular/core';
import { MatDateRangePicker } from '@angular/material/datepicker';
import { Constants } from '../../../../shared/interfaces/Constants';
import {ActivatedRoute, Router} from '@angular/router';
import { AccommodationService } from '../../../accommodation/services/accommodation.service';
import {DatePipe} from "@angular/common";
import {PropertyType} from "../../../../shared/interfaces/PropertyType";
import {MatCheckboxChange} from "@angular/material/checkbox";

@Component({
  selector: 'app-search-accommodation',
  templateUrl: './search-accommodation.component.html',
  styleUrl: './search-accommodation.component.css'
})
export class SearchAccommodationComponent implements OnInit {
  data: any;
  accommodations: any;
  types: Array<number> = [];
  stars: Array<number> = [];
  facilities: Array<string> = [];

  @ViewChild('picker') picker!: MatDateRangePicker<Date>;

  priceStartValue: any;
  priceEndValue: any;

  displayedAccommodationTypesList: Array<PropertyType> = [];
  showMoreTypes = false;
  filterAmenities = Constants.FilterAmenities;
  displayedFilterAmenities: Array<string> = [];
  accommodationTypes: Array<PropertyType> = [];
  showMoreAmenities = false;
  currentDate = new Date();

  filters: any;

  constructor(
    private route: ActivatedRoute,
    private datePipe: DatePipe,
    private router: Router,
    private accommodationService: AccommodationService) {}

  ngOnInit(): void {
      this.route.queryParams.subscribe(res => {
      this.data = {
        town: res['town'],
        startDate: this.datePipe.transform(res['startDate'], 'yyyy-MM-dd'),
        endDate: this.datePipe.transform(res['endDate'], 'yyyy-MM-dd'),
        rooms: res['rooms'],
        people: res['people'],
      };

        this.accommodationService
          .listing(res)
          .subscribe(accommodations => {
            this.accommodations = accommodations;

            let minPrice = 0;
            let maxPrice = 0;

            for (let accommodation of accommodations) {
              for (let roomConfiguration of accommodation.roomConfigurations) {
                let price = 0;
                roomConfiguration.forEach((room: any) => price += room.pricePerNight * this.calculateNights());

                if (minPrice === 0 || price <= minPrice) minPrice = price;
                if (price >= maxPrice) maxPrice = price;
              }
            }

            this.priceStartValue = minPrice;
            this.priceEndValue = maxPrice;

            console.log(minPrice);
            console.log(maxPrice);

            this.filters = {
              minPrice: minPrice,
              maxPrice: maxPrice,
              hasParking: false,
              hasPosTerminal: false,
              acceptPets: false,
              facilities: null,
              stars: null,
              types: null
            }
          });
    });

    this.accommodationService.getAccommodationTypes().subscribe(res => {
      this.accommodationTypes = res;

      this.displayedAccommodationTypesList = this.accommodationTypes.slice(0, 3);
      this.displayedFilterAmenities = this.filterAmenities.slice(0, 3);
    });
  }

  search(): void {
    this.data.startDate = this.datePipe.transform(this.data.startDate, 'yyyy-MM-dd');
    this.data.endDate = this.datePipe.transform(this.data.endDate, 'yyyy-MM-dd');

    this.accommodationService
      .listing(this.data)
      .subscribe(accommodations => {
        this.accommodations = accommodations;
      });
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

  facilityChange(event: MatCheckboxChange, facility: string): void {
    if (event.checked) {
      this.facilities.push(facility);
    } else {
      let index = this.facilities.indexOf(facility);
      if (index >= 0) {
        this.facilities.splice(index, 1);
      }
    }
  }

  filter(): void {
    this.accommodationService
      .filter({
        ...this.data,
        minPrice: this.filters.minPrice,
        maxPrice: this.filters.maxPrice,
        hasParking: this.filters.hasParking,
        hasPosTerminal: this.filters.hasPosTerminal,
        acceptPets: this.filters.acceptPets,
        stars: this.stars.join(' ') || '1 2 3 4 5',
        facilities: this.facilities.join(' ') || ' ,  ',
        types: this.types.join(' ') || this.accommodationTypes.map(type => type.id).join(' ')
      })
      .subscribe(accommodations => {
        this.accommodations = accommodations;
      });
  }

  details(id: number): void {
    let data = {
      ...this.data,
      id: id,
    }

    this.router.navigate(['/accommodation/details'], { queryParams: data });
  }

  openDatePicker() {
    if (this.picker) {
      this.picker.open();
    }
  }

  //Number of people input
  decrementGuests() {
    if (this.data.people > 1) {
      this.data.people--;
    }
  }

  incrementGuests() {
    this.data.people++;
  }

  decrementRooms() {
    if (this.data.rooms > 1) {
      this.data.rooms--;
    }
  }

  incrementRooms() {
    this.data.rooms++;
  }

  //See more types button
  toggleShowMoreTypes() {
    this.showMoreTypes = !this.showMoreTypes;
    if (this.showMoreTypes) {
      this.displayedAccommodationTypesList = this.accommodationTypes;
    } else {
      this.displayedAccommodationTypesList = this.accommodationTypes.slice(0, 3);
    }
  }

  calculateNights(): number {
    const startDate: Date = new Date(this.data.startDate);
    const endDate: Date = new Date(this.data.endDate);

    // Calculate the difference in milliseconds
    const differenceInMilliseconds: number = endDate.getTime() - startDate.getTime();

    // Convert milliseconds to days
    const differenceInDays: number = differenceInMilliseconds / (1000 * 60 * 60 * 24);

    return differenceInDays;
  }

  //See more amenities button
  toggleShowMoreAmenities(): void {
    this.showMoreAmenities = !this.showMoreAmenities;
    if (this.showMoreAmenities) {
      this.displayedFilterAmenities = this.filterAmenities;
    } else {
      this.displayedFilterAmenities = this.filterAmenities.slice(0, 3);
    }
  }

  sort(event: any): void {
    switch (event) {
      case 'nameDescending':
        this.accommodations = this.accommodations.sort((a: any, b: any) => a.name > b.name ? -1 : (a.name > b.name) ? 1 : 0)
          break;
      case 'nameAscending':
        this.accommodations = this.accommodations.sort((a: any, b: any) => a.name < b.name ? -1 : (a.name > b.name) ? 1 : 0)
        break;
      case 'priceDescending':
        this.accommodations = this.accommodations.sort((a: any, b: any) => a.cheapestOption > b.cheapestOption ? -1 : (a.cheapestOption > b.cheapestOption) ? 1 : 0)
        break;
      case 'priceAscending':
        this.accommodations = this.accommodations.sort((a: any, b: any) => a.cheapestOption < b.cheapestOption ? -1 : (a.cheapestOption > b.cheapestOption) ? 1 : 0)
        break;
    }
  }
}
