import {Component, OnInit, ViewChild} from '@angular/core';
import { ChartComponent } from 'ng-apexcharts';
import { ChartOptions } from '../../../restaurant/components/manage/manage.component';
import { MatDialog } from '@angular/material/dialog';
import { GalleryItem, ImageItem } from 'ng-gallery';
import { ConfirmDeleteReservationComponent } from './dialogs/confirm-delete-reservation/confirm-delete-reservation.component';
import { PastReservationsComponent } from './dialogs/past-reservations/past-reservations.component';
import { GeneralInformationDialogComponent } from './dialogs/general-information-dialog/general-information-dialog.component';
import { ActivatedRoute, Router } from '@angular/router';
import { GalleryDialogComponent } from './dialogs/gallery-dialog/gallery-dialog.component';
import { AddEatingDialogComponent } from './dialogs/add-eating-dialog/add-eating-dialog.component';
import { EditDetailsComponent } from './dialogs/edit-details/edit-details.component';
import {AccommodationService} from "../../services/accommodation.service";
import {ToastrService} from "ngx-toastr";
import {UserService} from "../../../auth/services/user.service";
import {AddRoomDialogComponent} from "./dialogs/add-room-dialog/add-room-dialog.component";

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrl: './manage.component.css'
})
export class ManageComponent implements OnInit {
  data: any;
  accommodation: any;
  pastReservations: Array<any> = [];
  todayReservations: Array<any> = [];
  upcomingReservations: Array<any> = [];
  tabId: string = 'statistics';
  images: GalleryItem[] = [];
  meals: Array<any> = [];
  groupedFacilities: Array<any> = [];

  @ViewChild("chart") chart!: ChartComponent;
  public chartOptions: Partial<ChartOptions>;

  constructor(private route: ActivatedRoute,
    private userService: UserService,
    private accommodationService: AccommodationService,
    private toastrService: ToastrService,
    private router: Router,
    public dialog: MatDialog,) {
    this.route.queryParams.subscribe(res => {
      this.data = {
        id: res['id']
      };
    });

    this.chartOptions = {
      series: [
        {
          name: "Резервации",
          data: [10, 41, 35, 51, 49, 62, 89, 91, 148, 67, 102, 78]
        }
      ],
      chart: {
        height: 350,
        type: "line",
        zoom: {
          enabled: false
        },
        toolbar: {
          show: false
        }
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: "straight"
      },
      grid: {
        row: {
          colors: ["#f3f3f3", "transparent"],
          opacity: 0.5
        }
      },
      xaxis: {
        categories: [
          "Януари",
          "Февруари",
          "Март",
          "Април",
          "Май",
          "Юни",
          "Юли",
          "Август",
          "Септември",
          "Октомври",
          "Ноември",
          "Декември"
        ]
      }
    };
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.tabId = params['tabId'] || 'statistics';
    });

    this.accommodationService
      .details(this.data)
      .subscribe(res => {
        this.accommodation = res;

        this.accommodation.images.forEach((i: any) => this.images.push(new ImageItem({ src: i.url, thumb: i.url })));

        this.groupFacilitiesByCategory();
        console.log(this.accommodation);

        // this.restaurant.tables.forEach((table: any) => {
        //   let index: number = this.tableTypes.findIndex(t => t.capacity === table.capacity && t.isOutdoor === table.isOutdoor && t.isSmokingAllowed == table.isSmokingAllowed);
        //   if (index === -1) {
        //     this.tableTypes.push({
        //       capacity: table.capacity,
        //       isSmokingAllowed: table.isSmokingAllowed,
        //       isOutdoor: table.isOutdoor,
        //       tables: [table]
        //     });
        //   } else {
        //     this.tableTypes[index].tables.push(table);
        //   }
        // });
        //
        // console.log(this.tableTypes);
        //
        this.accommodationService
          .getReservationsByAccommodationId(this.accommodation.id)
          .subscribe((res: any) => {
            console.log(res);
            for (let reservation of res) {

              this.userService
                .getUser(reservation.clientId)
                .subscribe(res => {
                  reservation.client = res;

                  let compareDate = new Date(reservation.startDate);

                  let today = new Date();

                  if (compareDate > today) {
                    this.upcomingReservations.push(reservation);
                  } else if (compareDate < today) {
                    this.pastReservations.push(reservation);
                  } else {
                    this.todayReservations.push(reservation);
                  }
                });
            }
          });
      });
  }

  changeTab(tabId: string) {
    this.tabId = tabId;
    this.router.navigate([], {
        relativeTo: this.route,
        queryParams: { tabId: this.tabId },
        queryParamsHandling: 'merge'
    });
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

  setAsCoverImage(url: any): void {
    this.accommodation.coverPhotoUrl = url;
    this.accommodationService.edit(this.accommodation.id, this.accommodation).subscribe();
    //this.toastrService.info('Снимката е успешно зададена като основна снимка на обекта.')
  }

  deleteImage(index: number): void {
    let image = this.accommodation.images[index];

    this.accommodationService.deleteImage(image.id).subscribe();
  }

  changeVisibility(): void {
    this.accommodationService.changeVisibility(this.accommodation.id).subscribe((res: boolean)=> {
      if (res) {
        location.reload();
      }
    });
  }

  openPastReservationsDialog(): void {
    this.dialog.open(PastReservationsComponent, {
      width: '40rem',
      data: this.pastReservations
    });
  }

  openConfirmDeleteReservationDialog(): void {
    this.dialog.open(ConfirmDeleteReservationComponent, {
    });
  }

  openGeneralInfoDialog(): void {
    this.dialog.open(GeneralInformationDialogComponent, {
      width: '50rem',
      data: this.accommodation
    });
  }

  openGalleryUploadDialog(): void {
    this.dialog.open(GalleryDialogComponent, {
      width: '50rem',
      data: this.accommodation.id
    });
  }

  openEditDetailsDialog(): void {
    this.dialog.open(EditDetailsComponent, {
      width: '60rem'
    });
  }

  openAddRoomDialog(): void {
    this.dialog.open(AddRoomDialogComponent, {
      width: '36rem',
      height: '100%',
      position: { right:'0', top: '0' },
      data: { id: this.accommodation.id }
    });
  }

  openEditRoomDialog(): void {
    this.dialog.open(AddRoomDialogComponent, {
      width: '40rem'
    });
  }

  openAddEatingDialog(): void {
    this.dialog.open(AddEatingDialogComponent, {
      width: '50rem'
    });
  }

  openEditEatingDialog(): void {
    this.dialog.open(AddEatingDialogComponent, {
      width: '30rem'
    });
  }

  removeEating(meal: any): void {
    let index = this.meals.indexOf(meal);

    if (index !== -1) {
      this.meals.splice(index, 1);
    }
  }

}
