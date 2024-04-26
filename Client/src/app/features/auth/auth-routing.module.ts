import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AuthLayoutComponent } from '../../shared/layouts/auth-layout/auth-layout.component';
import { EmailVerificationComponent } from './components/email-verification/email-verification.component';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { ForgottenPasswordComponent } from './components/forgotten-password/forgotten-password.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { ProfileComponent } from './components/profile/profile.component';
import { AppLayoutComponent } from '../../shared/layouts/app-layout/app-layout.component';
import { ChangeEmailComponent } from './components/change-email/change-email.component';
import { AuthGuardService } from '../../core/guards/auth-guard.service';

const routes: Routes = [
  {
    path: 'auth',
    component: AuthLayoutComponent,
    children: [
      {
        path: 'register',
        component: RegisterComponent
      },
      {
        path: 'email-verification',
        component: EmailVerificationComponent
      },
      {
        path: 'confirm-email',
        component: ConfirmEmailComponent
      },
      {
        path: 'change-email',
        component: ChangeEmailComponent
      },
      {
        path: 'login',
        component: LoginComponent
      },
      {
        path: 'forgotten-password',
        component: ForgottenPasswordComponent
      },
      {
        path: 'change-password',
        component: ChangePasswordComponent
      }
    ]
  },
  {
    path: 'auth',
    component: AppLayoutComponent,
    children: [
      {
        path: 'profile',
        component: ProfileComponent,
        canActivate: [AuthGuardService]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
