import {Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css'
})
export class ChangePasswordComponent implements OnInit {
  userId!: string;
  token!: string;
  changePasswordForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private toastrService: ToastrService,
    private route: ActivatedRoute,
    private router: Router) {
    this.changePasswordForm = this.fb.group({
      'newPassword': ['', [Validators.required, Validators.minLength(8)]],
      'confirmNewPassword': ['', [Validators.required, Validators.minLength(8)]]
    });
  }

  ngOnInit() {
    this.route.queryParams.subscribe(res => {
      this.userId =  res['id'];
      this.token = res['token'];
    });
  }

  change(): void {
    if (this.changePasswordForm.invalid) {
      this.toastrService.error('Една или повече валидации не преминаха успешно.');
      return;
    }

    this.authService
      .changePassword({
        token: this.token,
        userId: this.userId,
        newPassword: this.changePasswordForm.value.newPassword
      })
      .subscribe(res => {
        this.router
          .navigate(['/auth/login'])
          .then(() => this.toastrService.success('Вашата парола бе обновена успешно.'));
      });
  }

  // Get form fields
  get newPassword() {
    return this.changePasswordForm.get('newPassword');
  }

  get confirmNewPassword() {
    return this.changePasswordForm.get('confirmNewPassword');
  }
}
