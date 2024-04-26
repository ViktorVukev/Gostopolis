import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import { GoogleMap } from '@angular/google-maps';
import { GalleryItem, ImageItem } from 'ng-gallery';
import { RoomDialogComponent } from './room-dialog/room-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import {ActivatedRoute, Router} from "@angular/router";
import {AccommodationService} from "../../services/accommodation.service";
import {UserService} from "../../../auth/services/user.service";
import {User} from "../../../../shared/interfaces/User";
import {AuthService} from "../../../auth/services/auth.service";
import {Constants} from "../../../../shared/interfaces/Constants";

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrl: './details.component.css'
})
export class DetailsComponent implements OnInit, AfterViewInit {
  isAuthenticated: boolean = this.authService.isAuthenticated();
  roomTypes: Array<string> = Constants.RoomTypesList;
  roomConfigurations: Array<any> = [];
  data: any;
  nights: number = 0;
  images: GalleryItem[] = [];
  accommodation: any;
  user: any;

  @ViewChild(GoogleMap, { static: false }) map!: GoogleMap;

  zoom = 16;
  center!: google.maps.LatLngLiteral;
  options: google.maps.MapOptions = {
    scrollwheel: false,
    disableDefaultUI: true,
    disableDoubleClickZoom: true,
    mapTypeId: 'terrain',
  };
  groupedFacilities: Array<any> = [];

  constructor(
    public dialog: MatDialog,
    private userService: UserService,
    private authService: AuthService,
    private router: Router,
    private accommodationService: AccommodationService,
    private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(res => {
      this.data = res;
      this.nights = this.calculateNights();

      this.accommodationService
        .details(res)
        .subscribe(accommodation => {
          this.accommodation = accommodation;
          this.accommodation.meals = this.accommodation.meals.sort((a:any, b:any) => (new Date(a.startTime) > new Date(b.startTime)) ? -1 : 1)

          this.center = {
            lat: this.accommodation.latitude,
            lng: this.accommodation.longitude
          };

          accommodation.images.forEach((i: any) => this.images.push(new ImageItem({ src: i.url, thumb: i.url })));

          this.groupFacilitiesByCategory();

          if (accommodation.roomConfigurations !== null) {
            this.setRoomConfigurations();
          }
        });
    });

    if (this.isAuthenticated) {
      this.userService.getCurrentUser().subscribe(res => {
        this.user = res;
      });
    }
  }

  ngAfterViewInit(): void {
    const marker = new google.maps.Marker({
      position: {
        lat: this.accommodation.latitude,
        lng: this.accommodation.longitude
      },
      map: this.map.googleMap
    });
  }

  openRoomDialog(room: any) {
    this.dialog.open(RoomDialogComponent, {
      width: '25rem',
      data: room
    });
  }

  isCurrentUserOwner(): boolean {
    return this.user.id === this.accommodation.partnerId;
  }

  changeVisibility(): void {
    this.accommodationService.changeVisibility(this.accommodation.id).subscribe((res: boolean)=> {
      if (res) {
        location.reload();
      }
    });
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

  groupFacilitiesByCategory(): void {
    this.accommodation.facilities.split(';').forEach((facility: string) => {
      let [category, facilityName] = facility.split(': ').map(item => item.trim());

      if (!this.groupedFacilities.some(f => f.name === category)) {
        this.groupedFacilities.push({
          name: category,
          facilities: []
        });
      }

      let index = this.groupedFacilities.findIndex(f => f.name === category);

      this.groupedFacilities[index].facilities.push(facilityName);
    });
  }

  setRoomConfigurations(): void {
    for (let accommodationConfiguration of this.accommodation.roomConfigurations) {
      let configuration: Array<any> = [];
      let price = 0;
      for (let configurationRoom of accommodationConfiguration) {

        let roomIndex = configuration.findIndex((r: any) => r.name === configurationRoom.name)
        if (roomIndex === -1) {
          configuration.push({
            ...configurationRoom,
            count: 1
          });
        } else {
          configuration[roomIndex].count++;
        }

        price += configurationRoom.pricePerNight * this.nights;
      }

      this.roomConfigurations.push({
        configuration: configuration,
        price: price
      });
    }
  }

  reserve(index: number): void {
    let data = {
        optionIndex: index,
        startDate: this.data.startDate,
        endDate: this.data.endDate,
        people: this.data.people,
        rooms: this.data.rooms,
        nights: this.nights,
        town: this.data.town,
        id: this.accommodation.id
    }

    this.router.navigate(['/accommodation/create-reservation'], { queryParams: data });
  }
}
