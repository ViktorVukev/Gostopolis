import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-email-verification',
  templateUrl: './email-verification.component.html',
  styleUrl: './email-verification.component.css'
})
export class EmailVerificationComponent implements OnInit, OnDestroy {
  isButtonDisabled: boolean = false;
  timeRemaining: number = 0;
  private resendTimeout: any;

  constructor(
    private authService: AuthService,
    private toastrService: ToastrService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    const savedTimeRemaining = parseInt(localStorage.getItem('resendTimer') || '0', 10);
    if (savedTimeRemaining > 0) {
      this.isButtonDisabled = true;
      this.timeRemaining = savedTimeRemaining;
      this.startTimer();
    }
  }

  resend(): void {
    this.route.queryParams.subscribe(res => {
      this.authService
        .resend(res['userId'])
        .subscribe(() => this.toastrService.info('На посоченият от Вас имейл бе повторно изпратен линк за верификация.'));
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

    localStorage.setItem('resendTimer', this.timeRemaining.toString());

    this.startTimer();
  }

  private startTimer(): void {
    this.resendTimeout = setInterval(() => {
      this.timeRemaining--;

      localStorage.setItem('resendTimer', this.timeRemaining.toString());

      if (this.timeRemaining <= 0) {
        clearInterval(this.resendTimeout);
        this.isButtonDisabled = false;
        localStorage.removeItem('resendTimer');
      }
    }, 1000);
  }

  ngOnDestroy(): void {
    if (this.resendTimeout) {
      clearInterval(this.resendTimeout);
    }
  }
}
