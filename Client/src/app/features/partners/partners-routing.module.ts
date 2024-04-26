import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppLayoutComponent } from '../../shared/layouts/app-layout/app-layout.component';
import { ApplyComponent } from './components/apply/apply.component';
import { AuthGuardService } from '../../core/guards/auth-guard.service';
import { ApplicationsComponent } from './components/applications/applications.component';
import { CreatePropertyComponent } from './components/create-property/create-property.component';
import { ReservationsComponent } from './components/reservations/reservations.component';

const routes: Routes = [
  {
    path: 'partners',
    component: AppLayoutComponent,
    canActivate: [AuthGuardService],
    children: [
      {
        path: 'apply',
        component: ApplyComponent,
      },
      {
        path: 'applications',
        component: ApplicationsComponent
      },
      {
        path: 'create-property',
        component: CreatePropertyComponent
      },
      {
        path: 'reservations',
        component: ReservationsComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class PartnersRoutingModule { }
