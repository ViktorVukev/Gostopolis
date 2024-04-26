import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { TableType } from '../../../../../../shared/interfaces/TableType';
import { Table } from '../../../../../../shared/interfaces/Table';

@Component({
  selector: 'app-add-table-dialog',
  templateUrl: './add-table-dialog.component.html',
  styleUrl: './add-table-dialog.component.css'
})
export class AddTableDialogComponent {

  isSmokingAllowed: boolean = false;
  isOutdoor: boolean = false;
  tableForm: FormGroup;

  constructor(public dialogRef: MatDialogRef<AddTableDialogComponent>,
    private formBuilder: FormBuilder) {
      this.tableForm = this.formBuilder.group({
        'capacity': [null, [Validators.min(1), Validators.required]],
        'tableCount': [null, [Validators.min(1), Validators.required]],
      });
  }

  onSubmit(): void {
    if (this.tableForm.invalid) {
      return;
    }

    let formData = this.tableForm.value;
    let tables: Array<Table> = [];

    for (let i = 0; i < formData.tableCount; i++) {
      tables.push({
          id: crypto.randomUUID(),
          number: '',
          capacity: formData.capacity,
          isSmokingAllowed: this.isSmokingAllowed,
          isOutdoor: this.isOutdoor,
      });
    }

    let type: TableType = {
        capacity: formData.capacity,
        isSmokingAllowed: this.isSmokingAllowed,
        isOutdoor: this.isOutdoor,
        tables: tables
    };

    this.dialogRef.close(type);
  }

  // Get form fields
  get capacity() {
    return this.tableForm.get('capacity');
  }

  // Get form fields
  get tableCount() {
    return this.tableForm.get('tableCount');
  }
}
