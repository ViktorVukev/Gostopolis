import { Component } from '@angular/core';
import { Constants } from '../../../../../shared/interfaces/Constants';
import {MatCheckboxChange} from "@angular/material/checkbox";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MatDialogRef} from "@angular/material/dialog";
import {Room} from "../../../../../shared/interfaces/Room";

@Component({
  selector: 'app-add-room-dialog',
  templateUrl: './add-room-dialog.component.html',
  styleUrl: './add-room-dialog.component.css'
})
export class AddRoomDialogComponent {
  createForm: FormGroup;
  showAddBed: boolean = false;
  showAddBathroomAmenity: boolean = false;
  showAddRoomAmenity: boolean = false;
  numberOfBeds: number = 1;
  bedTypeIndex: number = 0;
  roomTypes: Array<string> = Constants.RoomTypesList;
  bedTypes: Array<string> = Constants.BedTypesList;
  allBathroomAmenitiesList: string[] = Constants.BathroomAmenitiesList;
  allRoomAmenitiesList: Array<any> = Constants.RoomAmenitiesList;
  roomAmenities: Array<string> = [];
  bathroomAmenities: Array<string> = [];
  beds: Array<string> = [];

  constructor(
    public dialogRef: MatDialogRef<AddRoomDialogComponent>,
    private fb: FormBuilder) {
    this.createForm = this.fb.group({
      'type': [null, Validators.required],
      'name': ['', Validators.required],
      'pricePerNight': [0, Validators.required],
      'capacity': [0, Validators.required],
      'count': [1, Validators.required],
      'hasPrivateBathroom': [false, Validators.required]
    });
  }

  openAddBed(): void {
    this.showAddBed = !this.showAddBed;
  }

  addBed(): void {
    let index: number = this.beds.findIndex(b => b.startsWith(this.bedTypeIndex.toString()))

    if (index === -1){
      this.beds.push(this.bedTypeIndex + ': ' + this.numberOfBeds);
    } else {
      let number = parseInt(this.beds[index].split(': ')[1]);
      number += this.numberOfBeds;
      this.beds[index] = `${this.bedTypeIndex}: ${number}`;
    }
  }

  deleteBed(index: number): void {
    if (index !== -1) {
      this.beds.splice(index, 1);
    }
  }

  changeBedType(index: number): void {
    this.bedTypeIndex = index;
  }

  addBathroomAmenity() {
    this.showAddBathroomAmenity = !this.showAddBathroomAmenity;
  }

  bathroomAmenityChange(event: MatCheckboxChange, amenity: string): void {
    if (event.checked) {
      this.bathroomAmenities.push(amenity);
    } else {
      let index = this.bathroomAmenities.indexOf(amenity);
      if (index >= 0) {
        this.bathroomAmenities.splice(index, 1);
      }
    }
  }

  roomAmenityChange(event: MatCheckboxChange,category: string, facility: string): void {
    if (event.checked) {
      this.roomAmenities.push(category + ': ' + facility);
    } else {
      let index = this.roomAmenities.indexOf(facility);
      if (index >= 0) {
        this.roomAmenities.splice(index, 1);
      }
    }
  }

  create(): void {
    if (this.createForm.invalid) {
      return;
    }

    let rooms: Array<Room> = [];

    for (let i = 0; i < this.createForm.value.count; i++) {
      rooms.push({
        id: crypto.randomUUID(),
        number: '',
        floor: null,
        ...this.createForm.value,
        bathroomAmenities: this.bathroomAmenities.join(';'),
        roomAmenities: this.roomAmenities.join(';'),
        beds: this.beds.join(';')
      });
    }

    this.dialogRef.close(rooms);
  }

  addRoomAmenity() {
    this.showAddRoomAmenity = !this.showAddRoomAmenity;
  }

  decrementBeds() {
    if (this.numberOfBeds > 1) {
      this.numberOfBeds--;
    }
  }

  incrementBeds() {
    if (this.numberOfBeds < 10) {
      this.numberOfBeds++;
    }
  }

  // Get form fields
  get name() {
    return this.createForm.get('name');
  }

  get pricePerNight() {
    return this.createForm.get('pricePerNight');
  }

  get capacity() {
    return this.createForm.get('capacity');
  }

  get count() {
    return this.createForm.get('count');
  }

  get type() {
    return this.createForm.get('type');
  }

  protected readonly parseInt = parseInt;
}
