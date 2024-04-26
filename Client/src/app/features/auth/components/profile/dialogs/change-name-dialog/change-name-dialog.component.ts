import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../../services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-change-name-dialog',
  templateUrl: './change-name-dialog.component.html',
  styleUrl: './change-name-dialog.component.css'
})
export class ChangeNameDialogComponent implements OnInit {
  updateForm: FormGroup;
  user: any;

  constructor(
    private userService: UserService,
    private toastrService: ToastrService,
    private fb: FormBuilder) {
    this.updateForm = this.fb.group({
      'firstName': ['', [Validators.required, Validators.minLength(2), Validators.maxLength(30)]],
      'lastName': ['', [Validators.required, Validators.minLength(2), Validators.maxLength(30)]],
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
        this.toastrService.success('Успешно променихте своите имена.');
        setTimeout(() => location.reload(), 1000)
      });
  }

  // Get form fields
  get firstName() {
    return this.updateForm.get('firstName');
  }

  get lastName() {
    return this.updateForm.get('lastName');
  }
}
