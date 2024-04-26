import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../../services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-change-profile-image-dialog',
  templateUrl: './change-profile-image-dialog.component.html',
  styleUrl: './change-profile-image-dialog.component.css'
})
export class ChangeProfileImageDialogComponent implements OnInit {
  fileName: string = 'Избери файл';
  file: any;
  updateForm!: FormGroup;
  user: any;

  constructor(
    private userService: UserService,
    private toastrService: ToastrService,
    private fb: FormBuilder) {
    this.updateForm = this.fb.group({
      'imageBase64': ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.userService
      .getCurrentUser()
      .subscribe(res => {
        this.user = res;
      });
    }

  upload(event: any): void {
    const file: File = <File>event.target.files[0];

    if (file.size > 10485760) {
      this.toastrService.error('Файлът не трябва да надвишава 10MB.');
      return;
    }

    this.file = file;
    this.fileName = this.file.name;
    this.toastrService.info('Файлът е прикачен.');
  }

  update(): void {
    if (this.file) {
      let reader = new FileReader();
      reader.readAsDataURL(this.file);

      reader.onload = (_event) => {
        this.userService
          .updateImage({
            imageBase64: reader.result as string
          })
          .subscribe(() => {
            this.toastrService.success('Профилната Ви снимка бе обновена успешно.');
            setTimeout(() => location.reload(), 1000)
          });
      };
    }
  }
}
