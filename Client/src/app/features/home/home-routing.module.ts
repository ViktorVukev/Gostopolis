import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './components/index/index.component';
import { AppLayoutComponent } from '../../shared/layouts/app-layout/app-layout.component';
import { AuthGuardService } from '../../core/guards/auth-guard.service';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { SearchAccommodationComponent } from './components/search-accommodation/search-accommodation.component';
import { SearchRestaurantComponent } from './components/search-restaurant/search-restaurant.component';
import { MyReservationsComponent } from './components/my-reservations/my-reservations.component';
import { ReservationDetailsComponent } from './components/my-reservations/reservation-details/reservation-details.component';
import { UserManualComponent } from './components/user-manual/user-manual.component';


const routes: Routes = [
  {
    path: '',
    component: AppLayoutComponent,
    children: [
      {
        path: '',
        component: IndexComponent
      },
      {
        path: 'user-manual',
        component: UserManualComponent
      },
      {
        path: 'dashboard',
        component: DashboardComponent,
        canActivate: [AuthGuardService]
      },
      {
        path: 'search-accommodation',
        component: SearchAccommodationComponent
      },
      {
        path: 'search-restaurant',
        component: SearchRestaurantComponent
      },
      {
        path: 'reservations',
        component: MyReservationsComponent,
        canActivate: [AuthGuardService],
      },
      {
        path: 'reservation',
        component: ReservationDetailsComponent,
        canActivate: [AuthGuardService],
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class HomeRoutingModule { }
