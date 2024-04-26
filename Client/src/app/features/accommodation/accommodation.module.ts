import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './components/create/create.component';
import { MatStepperModule } from '@angular/material/stepper';
import { RouterModule } from '@angular/router';
import { AccommodationRoutingModule } from './accommodation-routing.module';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { AddRoomDialogComponent as AddRoomsDialogComponent} from './components/create/add-room-dialog/add-room-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { DetailsComponent } from './components/details/details.component';
import { GALLERY_CONFIG, GalleryConfig, GalleryModule } from 'ng-gallery';
import { MatIconModule } from '@angular/material/icon';
import { GoogleMapsModule } from '@angular/google-maps';
import { RoomDialogComponent } from './components/details/room-dialog/room-dialog.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { CreateReservationComponent } from './components/create-reservation/create-reservation.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDividerModule } from '@angular/material/divider';
import { GallerizeDirective } from "ng-gallery/lightbox";
import { ManageComponent } from './components/manage/manage.component';
import { NgApexchartsModule } from 'ng-apexcharts';
import { MatExpansionModule } from '@angular/material/expansion';
import { ConfirmDeleteReservationComponent } from './components/manage/dialogs/confirm-delete-reservation/confirm-delete-reservation.component';
import { PastReservationsComponent } from './components/manage/dialogs/past-reservations/past-reservations.component';
import { GeneralInformationDialogComponent } from './components/manage/dialogs/general-information-dialog/general-information-dialog.component';
import { GalleryDialogComponent } from './components/manage/dialogs/gallery-dialog/gallery-dialog.component';
import { EditDetailsComponent } from './components/manage/dialogs/edit-details/edit-details.component';
import { AddEatingDialogComponent } from './components/manage/dialogs/add-eating-dialog/add-eating-dialog.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatChipsModule } from '@angular/material/chips';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { QuillModule } from 'ngx-quill';
import { ReservationsComponent } from './components/reservations/reservations.component';
import {MatTableModule} from "@angular/material/table";
import { AddRoomDialogComponent } from "./components/manage/dialogs/add-room-dialog/add-room-dialog.component";

@NgModule({
  declarations: [
    CreateComponent,
    AddRoomsDialogComponent,
    AddRoomDialogComponent,
    DetailsComponent,
    RoomDialogComponent,
    CreateReservationComponent,
    ManageComponent,
    ConfirmDeleteReservationComponent,
    PastReservationsComponent,
    GeneralInformationDialogComponent,
    GalleryDialogComponent,
    EditDetailsComponent,
    AddEatingDialogComponent,
    CreateReservationComponent,
    ReservationsComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    AccommodationRoutingModule,
    MatStepperModule,
    MatButtonModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCardModule,
    MatDialogModule,
    MatSlideToggleModule,
    MatInputModule,
    GalleryModule,
    MatIconModule,
    GoogleMapsModule,
    MatTooltipModule,
    MatDatepickerModule,
    MatDividerModule,
    GallerizeDirective,
    NgApexchartsModule,
    MatExpansionModule,
    MatToolbarModule,
    MatChipsModule,
    MatAutocompleteModule,
    QuillModule,
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    AccommodationRoutingModule,
    MatStepperModule,
    MatButtonModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatCardModule,
    MatDialogModule,
    MatSlideToggleModule,
    MatInputModule,
    GalleryModule,
    MatIconModule,
    GoogleMapsModule,
    MatTooltipModule,
    MatDatepickerModule,
    MatDividerModule,
    GallerizeDirective,
    MatTableModule,
    MatExpansionModule
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
export class AccommodationModule { }
