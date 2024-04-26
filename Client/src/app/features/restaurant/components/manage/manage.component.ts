import {Component, OnInit, ViewChild} from '@angular/core';
import ApexCharts from 'apexcharts';
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexDataLabels,
  ApexTitleSubtitle,
  ApexStroke,
  ApexGrid
} from "ng-apexcharts";
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import { PastReservationsComponent } from './dialogs/past-reservations/past-reservations.component';
import { ConfirmDeleteReservationComponent } from './dialogs/confirm-delete-reservation/confirm-delete-reservation.component';
import { GalleryItem, ImageItem } from 'ng-gallery';
import { GeneralInformationDialogComponent } from './dialogs/general-information-dialog/general-information-dialog.component';
import { GalleryDialogComponent } from './dialogs/gallery-dialog/gallery-dialog.component';
import { EditDetailsDialogComponent } from './dialogs/edit-details-dialog/edit-details-dialog.component';
import { AddTableDialogComponent } from '../create/dialogs/add-table-dialog/add-table-dialog.component';
import { ProductDialogComponent } from '../create/dialogs/product-dialog/product-dialog.component';
import { MenuDialogComponent } from '../create/dialogs/menu-dialog/menu-dialog.component';
import {ActivatedRoute, Router} from "@angular/router";
import {RestaurantService} from "../../services/restaurant.service";
import {Product} from "../../../../shared/interfaces/Product";
import {UserService} from "../../../auth/services/user.service";
import {TableType} from "../../../../shared/interfaces/TableType";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  dataLabels: ApexDataLabels;
  grid: ApexGrid;
  stroke: ApexStroke;
  title: ApexTitleSubtitle;
};

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrl: './manage.component.css'
})
export class ManageComponent implements OnInit {
  data: any;
  restaurant: any;
  pastReservations: Array<any> = [];
  todayReservations: Array<any> = [];
  upcomingReservations: Array<any> = [];
  tabId: string = 'statistics';
  images: GalleryItem[] = [];
  tableTypes: Array<TableType> = [];

  @ViewChild("chart") chart!: ChartComponent;
  public chartOptions: Partial<ChartOptions>;

  constructor(
    public dialog: MatDialog,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private restaurantService: RestaurantService) {
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

    this.restaurantService
      .details(this.data)
      .subscribe(res => {
        this.restaurant = res.data;

        this.restaurant.images.forEach((i: any) => this.images.push(new ImageItem({ src: i.url, thumb: i.url })));

        console.log(this.restaurant);
        this.restaurant.tables.forEach((table: any) => {
          let index: number = this.tableTypes.findIndex(t => t.capacity === table.capacity && t.isOutdoor === table.isOutdoor && t.isSmokingAllowed == table.isSmokingAllowed);
          if (index === -1) {
            this.tableTypes.push({
              capacity: table.capacity,
              isSmokingAllowed: table.isSmokingAllowed,
              isOutdoor: table.isOutdoor,
              tables: [table]
            });
          } else {
            this.tableTypes[index].tables.push(table);
          }
        });

        console.log(this.tableTypes);

        this.restaurantService
          .getReservationsByRestaurantId(this.restaurant.id)
          .subscribe((res: any) => {
            for (let reservation of res.data) {
              reservation.tableNumbers = reservation.tables.map((t: any) => t.number).join(', ');

              this.userService
                  .getUser(reservation.clientId)
                  .subscribe(res => {
                    reservation.client = res;

                    let compareDate = new Date(reservation.startDate);

                    let today = new Date();

                    compareDate.setHours(0, 0, 0, 0);
                    today.setHours(0, 0, 0, 0);

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

  changeTab(tabId: string): void {
    this.tabId = tabId;
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { tabId: this.tabId },
      queryParamsHandling: 'merge'
    });
  }

  deleteImage(index: number): void {
    let image = this.restaurant.images[index];

    this.restaurantService.deleteImage(image.id).subscribe();
  }

  deleteTable(id: any): void {
    this.restaurantService.deleteTable(id).subscribe(res => {
      if(res) {
        location.reload();
      }
    });
  }

  deleteProduct(id: any): void {
    this.restaurantService.deleteProduct(id).subscribe(res => {
      if(res) {
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
      data: this.restaurant
    });
  }

  openGalleryUploadDialog(): void {
    this.dialog.open(GalleryDialogComponent, {
      width: '50rem',
        data: this.restaurant.id
    });
  }

  openEditDetailsDialog(): void {
    this.dialog.open(EditDetailsDialogComponent, {
      width: '40rem',
      data: this.restaurant
    });
  }

  openAddTableDialog(): void {
    this.dialog.open(AddTableDialogComponent, {
      width: '30rem'
    });
  }

  openEditTableDialog(): void {
    this.dialog.open(AddTableDialogComponent, {
      width: '30rem'
    });
  }

  openAddProductDialog(): void {
    const dialogRef: MatDialogRef<ProductDialogComponent> = this.dialog.open(ProductDialogComponent, {
      width: '30rem'
    });

    dialogRef.afterClosed().subscribe((result: Product) => {
      if (result) {
        this.restaurantService
          .createProduct(this.restaurant.id, result)
          .subscribe();
      }
    });
  }

  openEditProductDialog(): void {
    this.dialog.open(ProductDialogComponent, {
      width: '30rem'
    });
  }

  openAddMenuDialog(): void {
    this.dialog.open(MenuDialogComponent, {
      width: '30rem'
    });
  }

  changeVisibility(): void {
    this.restaurantService.changeVisibility(this.restaurant.id).subscribe((res: boolean)=> {
      if (res) {
        location.reload();
      }
    });
  }

    setAsCoverImage(url: any): void {
    this.restaurant.coverPhotoUrl = url;
    this.restaurantService.update(this.restaurant.id, this.restaurant).subscribe();
        //this.toastrService.info('Снимката е успешно зададена като основна снимка на обекта.')
    }
}
