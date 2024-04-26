import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule, AsyncPipe } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RestaurantRoutingModule } from './restaurant-routing.module';
import { CreateComponent } from './components/create/create.component';
import { MatStepperModule } from '@angular/material/stepper';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { AddTableDialogComponent } from './components/create/dialogs/add-table-dialog/add-table-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ProductDialogComponent } from './components/create/dialogs/product-dialog/product-dialog.component';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MenuDialogComponent } from './components/create/dialogs/menu-dialog/menu-dialog.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { DetailsComponent } from './components/details/details.component';
import { GALLERY_CONFIG, GalleryConfig, GalleryModule } from 'ng-gallery';
import { GoogleMapsModule } from '@angular/google-maps';
import { SharedModule } from "../../shared/shared.module";
import { CreateReservationComponent } from './components/create-reservation/create-reservation.component';
import { MatDividerModule } from '@angular/material/divider';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ReservationsComponent } from './components/reservations/reservations.component';
import { GallerizeDirective } from "ng-gallery/lightbox";
import { ManageComponent } from './components/manage/manage.component';
import { NgApexchartsModule } from 'ng-apexcharts';
import { PastReservationsComponent } from './components/manage/dialogs/past-reservations/past-reservations.component';
import { ConfirmDeleteReservationComponent } from './components/manage/dialogs/confirm-delete-reservation/confirm-delete-reservation.component';
import { GeneralInformationDialogComponent } from './components/manage/dialogs/general-information-dialog/general-information-dialog.component';
import { QuillModule } from 'ngx-quill';
import { GalleryDialogComponent } from './components/manage/dialogs/gallery-dialog/gallery-dialog.component';
import { EditDetailsDialogComponent } from './components/manage/dialogs/edit-details-dialog/edit-details-dialog.component';

@NgModule({
  declarations: [
    CreateComponent,
    AddTableDialogComponent,
    ProductDialogComponent,
    MenuDialogComponent,
    DetailsComponent,
    CreateReservationComponent,
    ManageComponent,
    PastReservationsComponent,
    ConfirmDeleteReservationComponent,
    GeneralInformationDialogComponent,
    GalleryDialogComponent,
    EditDetailsDialogComponent,
    AddTableDialogComponent,
    CreateReservationComponent,
    ReservationsComponent
  ],
    imports: [
        CommonModule,
        RouterModule,
        RestaurantRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        MatStepperModule,
        MatSlideToggleModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatButtonModule,
        MatCardModule,
        MatDialogModule,
        MatChipsModule,
        MatIconModule,
        MatAutocompleteModule,
        AsyncPipe,
        MatToolbarModule,
        MatExpansionModule,
        GalleryModule,
        GoogleMapsModule,
        SharedModule,
        MatDividerModule,
        MatDatepickerModule,
        GallerizeDirective,
      MatExpansionModule,
      NgApexchartsModule,
      QuillModule,
    ],
  providers: [
    {
      provide: GALLERY_CONFIG,
      useValue: {
        imageSize: 'cover',
        disableThumbs: true,
        thumbs: false
      } as GalleryConfig
    }
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class RestaurantsModule { }

