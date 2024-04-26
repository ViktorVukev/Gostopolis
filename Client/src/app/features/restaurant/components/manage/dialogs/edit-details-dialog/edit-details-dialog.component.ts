import {Component, Inject} from '@angular/core';
import { Constants } from '../../../../../../shared/interfaces/Constants';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {RestaurantService} from "../../../../services/restaurant.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-edit-details-dialog',
  templateUrl: './edit-details-dialog.component.html',
  styleUrl: './edit-details-dialog.component.css'
})
export class EditDetailsDialogComponent {
  updateForm: FormGroup;
  workingTime: any;
  daysOfWeek: Array<any> = Constants.DaysOfWeek;
  timeOptions: Array<string> = Constants.TimeOptions;

  isOpen: boolean[] = [];
  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    public dialogRef: MatDialogRef<EditDetailsDialogComponent>,
    private fb: FormBuilder,
    private toastrService: ToastrService,
    private restaurantsService: RestaurantService) {
    this.workingTime = {
      restaurantId: data.id,
      ...data.workingTime,
    }

    this.isOpen[0] = !!data.workingTime.mondayOpenTime;
    this.isOpen[1] = !!data.workingTime.tuesdayOpenTime;
    this.isOpen[2] = !!data.workingTime.wednesdayOpenTime;
    this.isOpen[3] = !!data.workingTime.thursdayOpenTime;
    this.isOpen[4] = !!data.workingTime.fridayOpenTime;
    this.isOpen[5] = !!data.workingTime.saturdayOpenTime;
    this.isOpen[6] = !!data.workingTime.sundayOpenTime;

    console.log(this.isOpen)

    this.updateForm = this.fb.group({
      'restaurantId': [data.id],
      'mondayOpenTime': [data.workingTime.mondayOpenTime ? data.workingTime.mondayOpenTime.slice(0, 5) : null],
      'mondayCloseTime': [data.workingTime.mondayCloseTime ? data.workingTime.mondayCloseTime.slice(0, 5) : null],
      'tuesdayOpenTime': [data.workingTime.tuesdayOpenTime ? data.workingTime.tuesdayOpenTime.slice(0, 5) : null],
      'tuesdayCloseTime': [data.workingTime.tuesdayCloseTime ? data.workingTime.tuesdayCloseTime.slice(0, 5) : null],
      'wednesdayOpenTime': [data.workingTime.wednesdayOpenTime ? data.workingTime.wednesdayOpenTime.slice(0, 5) : null],
      'wednesdayCloseTime': [data.workingTime.wednesdayCloseTime ? data.workingTime.wednesdayCloseTime.slice(0, 5) : null],
      'thursdayOpenTime': [data.workingTime.thursdayOpenTime ? data.workingTime.thursdayOpenTime.slice(0, 5) : null],
      'thursdayCloseTime': [data.workingTime.thursdayCloseTime ? data.workingTime.thursdayCloseTime.slice(0, 5) : null],
      'fridayOpenTime': [data.workingTime.fridayOpenTime ? data.workingTime.fridayOpenTime.slice(0, 5) : null],
      'fridayCloseTime': [data.workingTime.fridayCloseTime ? data.workingTime.fridayCloseTime.slice(0, 5) : null],
      'saturdayOpenTime': [data.workingTime.saturdayOpenTime ? data.workingTime.saturdayOpenTime.slice(0, 5) : null],
      'saturdayCloseTime': [data.workingTime.saturdayCloseTime ? data.workingTime.saturdayCloseTime.slice(0, 5) : null],
      'sundayOpenTime': [data.workingTime.sundayOpenTime ? data.workingTime.sundayOpenTime.slice(0, 5) : null],
      'sundayCloseTime': [data.workingTime.sundayCloseTime ? data.workingTime.sundayCloseTime.slice(0, 5) : null]
    });

    for (let day of this.daysOfWeek) {
      this.displayOpenTime(day.english);
      this.displayCloseTime(day.english);
    }
  }

  remainingTimeOptions(day: string, filter: string): any {
    switch (filter) {
      case 'earlier':
        if (this.updateForm.get(day.toLowerCase() + 'CloseTime')?.value !== null) {
          return this.timeOptions.filter(to => this.timeOptions.indexOf(to) < this.timeOptions.indexOf(this.updateForm.get(day.toLowerCase() + 'CloseTime')?.value));
        } else {
          return this.timeOptions;
        }
      case 'later':
        return this.timeOptions.filter(to => this.timeOptions.indexOf(to) > this.timeOptions.indexOf(this.updateForm.get(day.toLowerCase() + 'OpenTime')?.value));
    }
  }

  // Function to update validators based on whether the restaurant is open or not
  updateValidators(day: string, index: number): void {
    let openTime = this.updateForm.get(day.toLowerCase() + 'OpenTime');
    let closeTime = this.updateForm.get(day.toLowerCase() + 'CloseTime');

    if (openTime && closeTime) {
      // Clear existing validators
      openTime.clearValidators();
      closeTime.clearValidators();

      // Add validators based on the condition
      if (this.isOpen[index]) {
        openTime.setValidators([Validators.required]);
        closeTime.setValidators([Validators.required]);
      } else {
        openTime.setValue(null);
        closeTime.setValue(null);
      }

      // Update control's validity
      openTime.updateValueAndValidity();
      closeTime.updateValueAndValidity();
    }
  }

  displayOpenTime(day: string): void {
    this.workingTime[day.toLowerCase() + 'OpenTime'] = this.workingTime[day.toLowerCase() + 'OpenTime']?.slice(0, 5);
  }

  displayCloseTime(day: string): void {
    this.workingTime[day.toLowerCase() + 'CloseTime'] =  this.workingTime[day.toLowerCase() + 'CloseTime']?.slice(0, 5);
  }

  update(): void {
    if (this.updateForm.invalid) {
      this.toastrService.error('Моля, попълнете всички полета.');
      return;
    }

    this.restaurantsService.updateWorkingTime(this.updateForm.value).subscribe(() => {
      this.toastrService.success('Работното време е променено успешно.');
      this.dialogRef.close();
    });
  }
}
