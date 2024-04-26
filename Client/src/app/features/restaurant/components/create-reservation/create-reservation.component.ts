import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {DatePipe} from "@angular/common";
import {AccommodationService} from "../../../accommodation/services/accommodation.service";
import {AuthService} from "../../../auth/services/auth.service";
import {UserService} from "../../../auth/services/user.service";
import {RestaurantService} from "../../services/restaurant.service";

@Component({
  selector: 'app-create-reservation',
  templateUrl: './create-reservation.component.html',
  styleUrl: './create-reservation.component.css'
})
export class CreateReservationComponent implements OnInit {
  isAuthenticated: boolean = this.authService.isAuthenticated();
  tableConfiguration: Array<any> = [];
  data: any;
  restaurant: any;
  user: any;
  partner: any;
  tableIds = ''

  constructor(
    private route: ActivatedRoute,
    private datePipe: DatePipe,
    private router: Router,
    private restaurantService: RestaurantService,
    private authService: AuthService,
    private userService: UserService,
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(res => {
      this.data = {
        id: res['id'],
        startTime: res['startTime'],
        people: res['people'],
        tables: res['tables'],
        town: res['town'],
        optionIndex: res['optionIndex']
      };

      this.restaurantService
        .details(this.data)
        .subscribe(res => {
          this.restaurant = res.data;

          for (let table of this.restaurant.tableConfigurations[parseInt(this.data.optionIndex)]) {
            this.tableConfiguration.push(table);
          }
          this.userService.getUser(this.restaurant.partnerId).subscribe(user => {
            this.partner = user;
          });
        });
    });

    if (this.isAuthenticated) {
      this.userService.getCurrentUser().subscribe(res => {
        this.user = res;
      });
    }
  }

  reserve(): void {
    let data = {
      startDate: this.data.startTime,
      clientEmail: this.user.email,
      partnerEmail: this.partner.email,
      restaurantId: this.data.id,
      guests: this.data.people,
      tableIds: this.tableConfiguration.map(r => r.id).join(';')
    }

    this.restaurantService.reserve(data).subscribe(() => this.router.navigate(['/reservations']));
  }
}
