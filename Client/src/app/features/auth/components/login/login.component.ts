import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private toastrService: ToastrService,
    private authService: AuthService) {
    this.loginForm = this.fb.group({
      'email': ['', [Validators.required, Validators.email]],
      'password': ['', [Validators.required, Validators.minLength(8)]]
    });
  }

  login(): void {
    if (this.loginForm.invalid) {
      this.toastrService.error('Една или повече валидации не преминаха успешно.');
      return;
    }

    this.authService
      .login(this.loginForm.value)
      .subscribe(data => {
        if (data.token) {
          this.authService.saveToken(data.token);
          this.router
            .navigate(['/'])
            .then(() => this.toastrService.success('Успешно влязохте в профила си.'));
        }
      });
  }

  // Get form fields
  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }
}
