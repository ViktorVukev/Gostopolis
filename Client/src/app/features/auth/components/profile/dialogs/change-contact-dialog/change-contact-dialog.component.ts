import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../../services/auth.service';

@Component({
  selector: 'app-change-contact-dialog',
  templateUrl: './change-contact-dialog.component.html',
  styleUrl: './change-contact-dialog.component.css'
})
export class ChangeContactDialogComponent implements OnInit {
  updateForm: FormGroup;
  user: any;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private toastrService: ToastrService,
    private fb: FormBuilder) {
    this.updateForm = this.fb.group({
      'firstName': ['', [Validators.required, Validators.minLength(2), Validators.maxLength(30)]],
      'lastName': ['', [Validators.required, Validators.minLength(2), Validators.maxLength(30)]],
      'newEmail': ['', [Validators.required, Validators.email]],
      'phoneNumber': ['', [Validators.required, Validators.pattern('\\+[0-9]*')]]
    });
  }

  ngOnInit(): void {
    this.userService
      .getCurrentUser()
      .subscribe(res => {
        this.user = res;
        this.updateForm.patchValue({
          firstName: res.firstName,
          lastName: res.lastName,
          newEmail: res.email,
          phoneNumber: res.phoneNumber
        });
      });
    }

  update(): void {
    if (this.updateForm.invalid) {
      this.toastrService.error('Една или повече валидации не преминаха успешно.');
      return;
    }

    this.userService
      .update(this.user.id, this.updateForm.value)
      .subscribe(() => {
        this.toastrService.success('Успешно променихте своя телефонен номер.');
        setTimeout(() => location.reload(), 3000)
      });

    if (this.updateForm.value.email !== this.user.email) {
      this.authService
        .resetEmail(this.updateForm.value)
        .subscribe(() => this.toastrService.info('Изпратихме линк за потвърждение на новия Ви имейл.'));
    }
  }

  // Get form fields
  get email() {
    return this.updateForm.get('email');
  }

  get phoneNumber() {
    return this.updateForm.get('phoneNumber');
  }
}
