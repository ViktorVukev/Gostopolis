import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import {ActivatedRoute, Router} from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-forgotten-password',
  templateUrl: './forgotten-password.component.html',
  styleUrl: './forgotten-password.component.css'
})
export class ForgottenPasswordComponent implements OnDestroy {
  forgottenPasswordForm: FormGroup;
  isButtonDisabled: boolean = false;
  timeRemaining: number = 0;
  private resendTimeout: any;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private toastrService: ToastrService) {
    this.forgottenPasswordForm = this.fb.group({
      'email': ['', [Validators.required, Validators.email]]
    });
  }

  ngOnInit(): void {
    const savedTimeRemaining = parseInt(localStorage.getItem('sendTimer') || '0', 10);
    if (savedTimeRemaining > 0) {
      this.isButtonDisabled = true;
      this.timeRemaining = savedTimeRemaining;
      this.startTimer();
    }
  }

  send(): void {
    this.authService
      .resetPassword(this.forgottenPasswordForm.value)
      .subscribe(res => {
        this.toastrService.info('На посоченият от Вас имейл бе изпратен линк за смяна на паролата.');
        this.forgottenPasswordForm.reset();
      });

    this.initializeTimer();
  }

  formatTime(seconds: number): string {
    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;
    return `${minutes}:${remainingSeconds < 10 ? '0' : ''}${remainingSeconds}`;
  }

  private initializeTimer(): void {
    this.isButtonDisabled = true;
    this.timeRemaining = 60;

    localStorage.setItem('sendTimer', this.timeRemaining.toString());

    this.startTimer();
  }

  private startTimer(): void {
    this.resendTimeout = setInterval(() => {
      this.timeRemaining--;

      localStorage.setItem('sendTimer', this.timeRemaining.toString());

      if (this.timeRemaining <= 0) {
        clearInterval(this.resendTimeout);
        this.isButtonDisabled = false;
        localStorage.removeItem('sendTimer');
      }
    }, 1000);
  }

  ngOnDestroy(): void {
    if (this.resendTimeout) {
      clearInterval(this.resendTimeout);
    }
  }

  get email() {
    return this.forgottenPasswordForm.get('email');
  }
}
