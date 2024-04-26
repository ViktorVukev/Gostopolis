import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private toastrService: ToastrService,
    private router: Router) {
    this.registerForm = this.fb.group({
      'firstName': ['', [Validators.required, Validators.minLength(2), Validators.maxLength(30)]],
      'lastName': ['', [Validators.required, Validators.minLength(2), Validators.maxLength(30)]],
      'email': ['', [Validators.required, Validators.email]],
      'phoneNumber': ['', [Validators.required]],
      'password': ['', [Validators.required, Validators.minLength(8)]],
      'confirmPassword': ['', [Validators.required, Validators.minLength(8)]]
    });
  }

  register(): void {
    if (this.registerForm.invalid) {
      this.toastrService.error('Една или повече валидации не преминаха успешно.');
      return;
    }

    this.registerForm.patchValue({ phoneNumber: '+359' + this.registerForm.value.phoneNumber });

    this.authService
      .register(this.registerForm.value)
      .subscribe(res => {
        this.router.navigate(['/auth/email-verification'], { queryParams: { userId: res.userId}});
      });
  }

  // Get form fields
  get email() {
    return this.registerForm.get('email');
  }

  get phoneNumber() {
    return this.registerForm.get('phoneNumber');
  }

  get firstName() {
    return this.registerForm.get('firstName');
  }

  get lastName() {
    return this.registerForm.get('lastName');
  }

  get password() {
    return this.registerForm.get('password');
  }

  get confirmPassword() {
    return this.registerForm.get('confirmPassword');
  }
}
