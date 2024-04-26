import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './services/auth.service';
import { UserService } from "./services/user.service";
import { SharedModule } from '../../shared/shared.module';
import { EmailVerificationComponent } from './components/email-verification/email-verification.component';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { ForgottenPasswordComponent } from './components/forgotten-password/forgotten-password.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { ProfileComponent } from './components/profile/profile.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ChangeNameDialogComponent } from './components/profile/dialogs/change-name-dialog/change-name-dialog.component';
import { ChangeContactDialogComponent } from './components/profile/dialogs/change-contact-dialog/change-contact-dialog.component';
import { ChangePasswordDialogComponent } from './components/profile/dialogs/change-password-dialog/change-password-dialog.component';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { ChangeProfileImageDialogComponent } from './components/profile/dialogs/change-profile-image-dialog/change-profile-image-dialog.component';
import { ChangeNotificationsDialogComponent } from './components/profile/dialogs/change-notifications-dialog/change-notifications-dialog.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ChangeEmailComponent } from './components/change-email/change-email.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    EmailVerificationComponent,
    ConfirmEmailComponent,
    ForgottenPasswordComponent,
    ChangePasswordComponent,
    ProfileComponent,
    ChangeNameDialogComponent,
    ChangeContactDialogComponent,
    ChangePasswordDialogComponent,
    ChangeProfileImageDialogComponent,
    ChangeNotificationsDialogComponent,
    ChangeEmailComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule,
    AuthRoutingModule,
    MatFormFieldModule,
    MatIconModule,
    MatButtonModule,
    MatInputModule,
    MatDialogModule,
    MatSlideToggleModule,
    MatToolbarModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [
    AuthService,
    UserService
  ]
})
export class AuthModule { }
