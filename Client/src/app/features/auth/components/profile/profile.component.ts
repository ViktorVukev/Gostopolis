import { Component, OnDestroy, OnInit } from '@angular/core';
import { ChangeNameDialogComponent } from './dialogs/change-name-dialog/change-name-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { ChangeContactDialogComponent } from './dialogs/change-contact-dialog/change-contact-dialog.component';
import { ChangePasswordDialogComponent } from './dialogs/change-password-dialog/change-password-dialog.component';
import { ChangeNotificationsDialogComponent } from './dialogs/change-notifications-dialog/change-notifications-dialog.component';
import { ChangeProfileImageDialogComponent } from './dialogs/change-profile-image-dialog/change-profile-image-dialog.component';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit, OnDestroy {
  user: any;
  tabId: string = 'profile';
  isButtonDisabled: boolean = false;
  timeRemaining: number = 0;
  private resendTimeout: any;

  constructor(
    public dialog: MatDialog,
    private userService: UserService) { }

  ngOnInit(): void {
    this.userService
      .getCurrentUser()
      .subscribe(res => this.user = res);

    const savedTimeRemaining = parseInt(localStorage.getItem('sendTimer2') || '0', 10);
    if (savedTimeRemaining > 0) {
      this.isButtonDisabled = true;
      this.timeRemaining = savedTimeRemaining;
      this.startTimer();
    }
  }

  changeTab(tabId: string): void {
    this.tabId = tabId;
  }

  openProfileImageDialog(): void {
    this.dialog.open(ChangeProfileImageDialogComponent, {
      width: '20rem'
    });
  }

  openNameDialog(): void {
    this.dialog.open(ChangeNameDialogComponent, {
      width: '40rem'
    });
  }

  openContactDialog(): void {
    this.dialog.open(ChangeContactDialogComponent, {
      width: '40rem'
    });
  }

  openPasswordDialog(): void {
    this.dialog.open(ChangePasswordDialogComponent, {
      width: '20rem'
    });

    this.initializeTimer();
  }

  openNotificationsDialog() {
    this.dialog.open(ChangeNotificationsDialogComponent, {
      width: '30rem'
    });
  }

  formatTime(seconds: number): string {
    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;
    return `${minutes}:${remainingSeconds < 10 ? '0' : ''}${remainingSeconds}`;
  }

  private initializeTimer(): void {
    this.isButtonDisabled = true;
    this.timeRemaining = 60;

    localStorage.setItem('sendTimer2', this.timeRemaining.toString());

    this.startTimer();
  }

  private startTimer(): void {
    this.resendTimeout = setInterval(() => {
      this.timeRemaining--;

      localStorage.setItem('sendTimer2', this.timeRemaining.toString());

      if (this.timeRemaining <= 0) {
        clearInterval(this.resendTimeout);
        this.isButtonDisabled = false;
        localStorage.removeItem('sendTimer2');
      }
    }, 1000);
  }

  ngOnDestroy(): void {
    if (this.resendTimeout) {
      clearInterval(this.resendTimeout);
    }
  }
}
