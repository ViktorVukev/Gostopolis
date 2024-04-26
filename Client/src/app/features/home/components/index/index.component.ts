import { Component, ViewChild, OnInit, AfterViewInit } from '@angular/core';
import { AuthService } from '../../../auth/services/auth.service';
import { MatDateRangePicker } from '@angular/material/datepicker';
import { Constants } from '../../../../shared/interfaces/Constants';
import {Statistics} from "../../../../shared/interfaces/Statistics";
import {StatisticsService} from "../../services/statistics.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {DatePipe} from "@angular/common";
import {Router} from "@angular/router";

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  isAuthenticated: boolean = this.authService.isAuthenticated();
  currentDate = new Date();
  searchForm: FormGroup;
  @ViewChild('picker') picker!: MatDateRangePicker<Date>;
  valueGuests: number = 1;
  valueRooms: number = 1;
  valueRestaurantGuests: number = 1;
  valueTables: number = 1;
  timeOptions: Array<string> = Constants.TimeOptions;
  statistics!: Statistics;
  places = [
    { name: 'Хотели', iconClass: 'fa-hotel' },
    { name: 'Къщи за гости', iconClass: 'fa-house' },
    { name: 'Квартири', iconClass: 'fa-building' },
    { name: 'Ресторанти', iconClass: 'fa-utensils' },
    { name: 'Сладкарница', iconClass: 'fa-cookie-bite' },
    { name: 'Кафе', iconClass: 'fa-mug-hot' },
    { name: 'Нощни клубове', iconClass: 'fa-martini-glass-citrus' }
  ];
  counters: Array<any> = [];

  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private datePipe: DatePipe,
    private router: Router,
    private statisticsService: StatisticsService) {
    this.searchForm = this.fb.group({
      'town': ['', Validators.required],
      'startDate': [null, Validators.required],
      'endDate': [null]
    });
  }

  ngOnInit(): void {
    this.statisticsService.getStatistics().subscribe(res => {
      this.statistics = res;
      this.counters.push(
        { title: 'Заведения', count: 0, max: this.statistics.totalRestaurants },
        { title: 'Места за настаняване', count: 0, max: this.statistics.totalAccommodations },
        { title: 'Резервации', count: 0, max: this.statistics.totalReservations },
        { title: 'Партньори', count: 0, max: this.statistics.totalPartners })
      this.incrementCounters();
    });
  }

  searchAccommodation(): void {
    let data = {
      people: this.valueGuests,
      rooms: this.valueRooms,
      town: this.searchForm.value.town,
      startDate: this.datePipe.transform(this.searchForm.value.startDate, 'yyyy-MM-dd'),
      endDate: this.datePipe.transform(this.searchForm.value.endDate, 'yyyy-MM-dd')
    }

    this.router.navigate(['/search-accommodation'], { queryParams: data });
  }

  searchRestaurant(): void {
    let data = {
      people: this.valueRestaurantGuests,
      tables: this.valueTables,
      town: this.searchForm.value.town,
      startDate: this.datePipe.transform(this.searchForm.value.startDate, 'yyyy-MM-dd'),
      startTime: this.searchForm.value.endDate
    }

    this.router.navigate(['/search-restaurant'], { queryParams: data });
  }

  openDatePicker(): void {
    if (this.picker) {
      this.picker.open();
    }
  }

  //Number of people input
  decrementGuests(): void {
    if (this.valueGuests > 1) {
      this.valueGuests--;
    }
  }

  incrementGuests(): void {
    this.valueGuests++;
  }

  decrementRooms() {
    if (this.valueRooms > 1) {
      this.valueRooms--;
    }
  }

  incrementRooms(): void {
    this.valueRooms++;
  }

  //Number of tables input
  decrementRestaurantGuests() {
    if (this.valueRestaurantGuests > 1) {
      this.valueRestaurantGuests--;
    }
  }

  incrementRestaurantGuests() {
    if (this.valueRestaurantGuests < 10) {
      this.valueRestaurantGuests++;
    }
  }

  decrementTables() {
    if (this.valueTables > 1) {
      this.valueTables--;
    }
  }

  incrementTables() {
    if (this.valueTables < 10) {
      this.valueTables++;
    }
  }

  /* Counters section */
  incrementCounters():void {
    this.counters.forEach(el => {
      let countStop = setInterval(() => {
        if (el.count === el.max) {
          clearInterval(countStop);
        } else {
          el.count++;
        }

      }, 30);
    });
  }
}
