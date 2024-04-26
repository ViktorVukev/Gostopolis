import {Component, Inject} from '@angular/core';
import {MatCheckboxChange} from "@angular/material/checkbox";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Constants} from "../../../../../../shared/interfaces/Constants";
import {Room} from "../../../../../../shared/interfaces/Room";
import {AccommodationService} from "../../../../services/accommodation.service";

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
    @Inject(MAT_DIALOG_DATA) private data: any,
    private accommodationService: AccommodationService,
    public dialogRef: MatDialogRef<AddRoomDialogComponent>,
    private fb: FormBuilder) {
    this.createForm = this.fb.group({
      'type': [null, Validators.required],
      'name': ['', Validators.required],
      'pricePerNight': [0, Validators.required],
      'capacity': [0, Validators.required],
      'number': [null, Validators.required],
      'accommodationId': [data.id, Validators.required],
      'floor': [null, Validators.required],
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

    this.accommodationService
      .createRoom({
        ...this.createForm.value,
        bathroomAmenities: this.bathroomAmenities.join(';'),
        roomAmenities: this.roomAmenities.join(';'),
        beds: this.beds.join(';')
      }).subscribe();

    this.dialogRef.close();
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

  get number() {
    return this.createForm.get('number');
  }

  get floor() {
    return this.createForm.get('floor');
  }

  get type() {
    return this.createForm.get('type');
  }

  protected readonly parseInt = parseInt;
}
