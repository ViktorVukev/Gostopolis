import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../../services/auth.service';
import { UserService } from '../../../../services/user.service';

@Component({
  selector: 'app-change-password-dialog',
  templateUrl: './change-password-dialog.component.html',
  styleUrl: './change-password-dialog.component.css'
})
export class ChangePasswordDialogComponent implements OnInit{
  constructor(
    private userService: UserService,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.userService
      .getCurrentUser()
      .subscribe(res => this.authService
        .resetPassword({ email: res.email })
        .subscribe()
      );
    }
}
