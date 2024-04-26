import { Component, ElementRef, ViewChild } from '@angular/core';
import { PartnersService } from '../../services/partners.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-apply',
  templateUrl: './apply.component.html',
  styleUrl: './apply.component.css'
})
export class ApplyComponent {
  @ViewChild('fileInput') fileInput!: ElementRef;
  fileName: string = 'Избери файл';
  file: any;

  constructor(
    private partnersService: PartnersService,
    private toastrService: ToastrService,
    private router: Router) { }

  apply(): void {
    if (this.file) {
      let reader = new FileReader();
      reader.readAsDataURL(this.file);

      reader.onload = (_event) => {
        this.partnersService.create({
          documentBase64: reader.result as string
        }).subscribe(() => this.router.navigate(['/partners/applications']));
      };
    }
  }

  uploadFile(event: any): void {
    const file = <File>event.target.files[0];

    if (file.size > 10485760) {
      this.toastrService.error('Файлът не трябва да надвишава 10MB.');
      return;
    }

    this.file = file;
    this.fileName = this.file.name;
    this.toastrService.info('Файлът е прикачен.');
  }
}
