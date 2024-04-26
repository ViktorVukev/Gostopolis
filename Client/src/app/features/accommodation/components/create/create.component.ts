import {Component, OnInit} from '@angular/core';
import { Constants } from '../../../../shared/interfaces/Constants';
import { AddRoomDialogComponent } from './add-room-dialog/add-room-dialog.component';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import {MatAutocompleteSelectedEvent} from "@angular/material/autocomplete";
import {MatCheckboxChange} from "@angular/material/checkbox";
import {
  AddTableDialogComponent
} from "../../../restaurant/components/create/dialogs/add-table-dialog/add-table-dialog.component";
import {TableType} from "../../../../shared/interfaces/TableType";
import {Room} from "../../../../shared/interfaces/Room";
import {ImageItem} from "ng-gallery";
import {ActivatedRoute, Router} from "@angular/router";
import {AccommodationService} from "../../services/accommodation.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrl: './create.component.css'
})
export class CreateComponent implements OnInit {
  accommodationFacilitiesList: Array<any> = Constants.AccommodationFacilitiesList;
  facilities: Array<any> = [];
  timeOptions: Array<string> = Constants.TimeOptions;
  showAddEating: boolean = false;
  rooms: Array<any> = [];
  accommodation: any;
  mealForm: FormGroup;
  meals: Array<any> = [];

  constructor(
    private route: ActivatedRoute,
    private accommodationService: AccommodationService,
    private router: Router,
    private fb: FormBuilder,
    public dialog: MatDialog) {
    this.mealForm = this.fb.group({
      'name': ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      'startTime': [null, [Validators.required]],
      'endTime': [null, [Validators.required]]
    });
  }

  remainingTimeOptions(filter: string): any {
    switch (filter) {
      case 'earlier':
        if (this.mealForm.get('endTime')?.value !== null) {
          return this.timeOptions.filter(to => this.timeOptions.indexOf(to) < this.timeOptions.indexOf(this.mealForm.get('endTime')?.value));
        } else {
          return this.timeOptions;
        }
      case 'later':
        return this.timeOptions.filter(to => this.timeOptions.indexOf(to) > this.timeOptions.indexOf(this.mealForm.get('startTime')?.value));
    }
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(res => {
      this.accommodationService
        .details(res)
        .subscribe(a => this.accommodation = a);
    });
  }

  facilityChange(event: MatCheckboxChange,category: string, facility: string): void {
    if (event.checked) {
      this.facilities.push(category + ': ' + facility);
    } else {
      let index = this.facilities.indexOf(facility);
      if (index >= 0) {
        this.facilities.splice(index, 1);
      }
    }
  }

  create(): void {
    this.accommodation.facilities = this.facilities.join(';');

    this.accommodationService.edit(this.accommodation.id, this.accommodation).subscribe();

    for (let room of this.rooms) {
      this.accommodationService.createRoom({ ...room, accommodationId: this.accommodation.id }).subscribe();
    }

    for (let meal of this.meals) {
      this.accommodationService.createMeal({ ...meal, accommodationId: this.accommodation.id }).subscribe();
    }

    this.router.navigate(['/dashboard']);
  }

  openAddEating(): void {
    this.showAddEating = !this.showAddEating;
  }

  addEating(): void {
    this.meals.push(this.mealForm.value);
    this.mealForm.reset();
  }

  removeEating(meal: any): void {
    let index = this.meals.indexOf(meal);

    if (index !== -1) {
      this.meals.splice(index, 1);
    }
  }

  deleteRoom(index: number): void {
    if (index !== -1) {
      this.rooms.splice(index, 1);
    }
  }

  openAddRoomDialog(): void {
    const dialogRef: MatDialogRef<AddRoomDialogComponent> = this.dialog.open(AddRoomDialogComponent, {
      width: '36rem',
      height: '100%',
      position: { right:'0', top: '0' }
    });

    dialogRef.afterClosed().subscribe((result: Array<Room>) => {
      if (result) {
        this.rooms.push(...result);
      }

      console.log(this.rooms);
    });
  }
}
