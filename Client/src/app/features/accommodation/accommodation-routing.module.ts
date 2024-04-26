import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppLayoutComponent } from "../../shared/layouts/app-layout/app-layout.component";
import { CreateComponent } from './components/create/create.component';
import { DetailsComponent } from './components/details/details.component';
import { CreateReservationComponent } from './components/create-reservation/create-reservation.component';
import { ManageComponent } from './components/manage/manage.component';
import {AuthGuardService} from "../../core/guards/auth-guard.service";
import {ReservationsComponent} from "./components/reservations/reservations.component";

const routes: Routes = [
  {
    path: 'accommodation',
    component: AppLayoutComponent,
    children: [
      {
        path: 'create',
        component: CreateComponent,
        canActivate: [AuthGuardService]
      },
      {
        path: 'details',
        component: DetailsComponent
      },
      {
        path: 'create-reservation',
        component: CreateReservationComponent,
        canActivate: [AuthGuardService]
      },
      {
        path: 'manage',
        component: ManageComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class AccommodationRoutingModule { }
