import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../../services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-change-notifications-dialog',
  templateUrl: './change-notifications-dialog.component.html',
  styleUrl: './change-notifications-dialog.component.css'
})
export class ChangeNotificationsDialogComponent implements OnInit {
  updateForm: FormGroup;
  user: any;

  constructor(
    private userService: UserService,
    private toastrService: ToastrService,
    private fb: FormBuilder) {
    this.updateForm = this.fb.group({
      'loginNotification': [true, Validators.required]
    });
  }

  ngOnInit(): void {
    this.userService
      .getCurrentUser()
      .subscribe(res => {
        this.user = res;
        this.updateForm.patchValue({
          loginNotification: res.loginNotification
        });
      });
    }

  update(): void {
    if (this.updateForm.invalid) {
      this.toastrService.error('Една или повече валидации не преминаха успешно.');
      return;
    }

    this.userService
      .updateEmailPreferences(this.updateForm.value)
      .subscribe(() => {
        this.toastrService.success('Успешно променихте своите предпочитания за имейл известяване.');
        setTimeout(() => location.reload(), 1000)
      });
  }
}
