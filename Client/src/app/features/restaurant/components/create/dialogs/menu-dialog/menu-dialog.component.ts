import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import {Product} from "../../../../../../shared/interfaces/Product";
import {Menu} from "../../../../../../shared/interfaces/Menu";

@Component({
  selector: 'app-menu-dialog',
  templateUrl: './menu-dialog.component.html',
  styleUrl: './menu-dialog.component.css'
})
export class MenuDialogComponent {
  createForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<MenuDialogComponent>,
    private formBuilder: FormBuilder) {
    this.createForm = this.formBuilder.group({
      'name': ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]]
    });
  }
  onSubmit(): void {
    if (this.createForm.invalid) {
      return;
    }

    let formData = this.createForm.value;

    let menu: Menu = {
      name: formData.name,
      products: new Array<Product>()
    };

    this.dialogRef.close(menu);
  }

  // Get form fields
  get name() {
    return this.createForm.get('name');
  }
}
