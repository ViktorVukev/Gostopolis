import {Component, OnInit} from '@angular/core';
import {ImageItem} from "ng-gallery";
import {ActivatedRoute, Router} from "@angular/router";
import {AuthService} from "../../../auth/services/auth.service";
import {UserService} from "../../../auth/services/user.service";
import {AccommodationService} from "../../services/accommodation.service";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-create-reservation',
  templateUrl: './create-reservation.component.html',
  styleUrl: './create-reservation.component.css'
})
export class CreateReservationComponent implements OnInit {
  isAuthenticated: boolean = this.authService.isAuthenticated();
  roomConfiguration: Array<any> = [];
  totalPrice: number = 0;
  dateOfBirth: any;
  additionalInformation!: string;
  data: any;
  accommodation: any;
  user: any;
  partner: any;
  currentDate = new Date();

  constructor(
      private route: ActivatedRoute,
      private datePipe: DatePipe,
      private router: Router,
      private accommodationService: AccommodationService,
      private authService: AuthService,
      private userService: UserService,
  ) {
    this.currentDate = new Date(this.currentDate.getFullYear() - 18, this.currentDate.getMonth(), this.currentDate.getDate());
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(res => {
      this.data = res;

      this.accommodationService
          .details(res)
          .subscribe(accommodation => {
            this.accommodation = accommodation;

            for (let room of accommodation.roomConfigurations[parseInt(this.data.optionIndex)]) {
              this.roomConfiguration.push(room);
              this.totalPrice += room.pricePerNight * this.data.nights;
            }
            this.userService.getUser(accommodation.partnerId).subscribe(user => {
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
      dateOfBirth: this.datePipe.transform(this.dateOfBirth, 'yyyy-MM-dd'),
      ...this.data,
      clientEmail: this.user.email,
      partnerEmail: this.partner.email,
      additionalInformation: this.additionalInformation,
      accommodationId: this.data.id,
      guests: this.data.people,
      roomIds: this.roomConfiguration.map(r => r.id).join(';')
    }

    this.accommodationService.reserve(data).subscribe(() => this.router.navigate(['/reservations']));
  }
}
