import { Component, ViewChild } from '@angular/core';
import { GoogleMap } from '@angular/google-maps';
import { GalleryItem, ImageItem } from 'ng-gallery';
import {ActivatedRoute, Router} from "@angular/router";
import { RestaurantService } from '../../services/restaurant.service';
import { MatDialog } from '@angular/material/dialog';
import {Constants} from "../../../../shared/interfaces/Constants";
import {UserService} from "../../../auth/services/user.service";
import {AuthService} from "../../../auth/services/auth.service";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrl: './details.component.css'
})
export class DetailsComponent {
  isAuthenticated: boolean = this.authService.isAuthenticated();
  images: GalleryItem[] = [];
  productCategories: Array<string> = Constants.CategoriesList;
  isCurrentlyOpen: boolean = false;
  restaurant: any;
  user: any;
  data: any;

  @ViewChild(GoogleMap, { static: false }) map!: GoogleMap;

  zoom = 16;
  center!: google.maps.LatLngLiteral;
  options: google.maps.MapOptions = {
    scrollwheel: false,
    disableDefaultUI: true,
    disableDoubleClickZoom: true,
    mapTypeId: 'terrain',
  };

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private authService: AuthService,
    private router: Router,
    private datePipe: DatePipe,
    private restaurantsService: RestaurantService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(res => {
      this.data = {
        id: res['id'],
        startTime: this.datePipe.transform(res['startDate'], 'yyyy-MM-dd') + 'T' + res['startTime'],
        people: res['people'],
        tables: res['tables'],
        town: res['town']
      };

      this.restaurantsService
        .details(this.data)
        .subscribe(res => {
          this.restaurant = res.data;
          console.log(this.restaurant);
          if (this.restaurant.workingTime !== null) {
            this.isCurrentlyOpen = this.restaurantsService.checkIfRestaurantIsOpen(this.restaurant.workingTime);
          }

          this.center = {
            lat: this.restaurant.latitude,
            lng: this.restaurant.longitude
          };
          this.restaurant.images.forEach((i: any) => this.images.push(new ImageItem({ src: i.url, thumb: i.url })))
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
        lat: this.restaurant.latitude,
        lng: this.restaurant.longitude
      },
      map: this.map.googleMap
    });
  }

  /* Check if category is empty */
  hasProducts(menu: any, type: number): boolean {
    return menu.products.some((product: any) => product.type === type);
  }

  isCurrentUserOwner(): boolean {
    return this.user.id === this.restaurant.partnerId;
  }
  reserve(index: number): void {
    let data = {
      optionIndex: index,
      startDate: this.data.startDate,
      startTime: this.data.startTime,
      people: this.data.people,
      tables: this.data.tables,
      town: this.data.town,
      id: this.restaurant.id
    }

    this.router.navigate(['/restaurant/create-reservation'], { queryParams: data });
  }
}
