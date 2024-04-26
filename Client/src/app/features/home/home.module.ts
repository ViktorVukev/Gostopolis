import { NgModule } from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
import { IndexComponent } from './components/index/index.component';
import { MatButtonModule } from "@angular/material/button";
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MAT_DATE_LOCALE, MatNativeDateModule, MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatTabsModule } from '@angular/material/tabs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HomeRoutingModule } from './home-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { MatCardModule } from '@angular/material/card';
import { SearchAccommodationComponent } from './components/search-accommodation/search-accommodation.component';
import { StatisticsService } from "./services/statistics.service";
import { MatSliderModule } from '@angular/material/slider';
import { MatDividerModule } from '@angular/material/divider';
import { MatRadioModule } from '@angular/material/radio';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { SearchRestaurantComponent } from './components/search-restaurant/search-restaurant.component';
import { MyReservationsComponent } from './components/my-reservations/my-reservations.component';
import { ReservationDetailsComponent } from './components/my-reservations/reservation-details/reservation-details.component';
import { UserManualComponent } from './components/user-manual/user-manual.component';
import { MatExpansionModule } from '@angular/material/expansion';

@NgModule({
  declarations: [
    IndexComponent,
    DashboardComponent,
    SearchAccommodationComponent,
    SearchRestaurantComponent,
    MyReservationsComponent,
    ReservationDetailsComponent,
    UserManualComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    HomeRoutingModule,
    MatButtonModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatInputModule,
    MatMenuModule,
    MatOptionModule,
    MatSelectModule,
    MatTabsModule,
    ReactiveFormsModule,
    FormsModule,
    MatIconModule,
    MatNativeDateModule,
    MatCardModule,
    MatSliderModule,
    MatDividerModule,
    MatRadioModule,
    MatCheckboxModule,
    MatExpansionModule
  ],
  providers: [
    DatePipe,
    StatisticsService,
    { provide: MAT_DATE_LOCALE, useValue: 'bg-BG' }
  ]

})
export class HomeModule { }
