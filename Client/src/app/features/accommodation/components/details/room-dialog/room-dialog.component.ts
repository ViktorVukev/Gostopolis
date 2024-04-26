import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {Constants} from "../../../../../shared/interfaces/Constants";

@Component({
  selector: 'app-room-dialog',
  templateUrl: './room-dialog.component.html',
  styleUrl: './room-dialog.component.css'
})
export class RoomDialogComponent {
  room: any;
  bedTypes = Constants.BedTypesList;
  beds: Array<any> = [];
  roomAmenities: Array<string> = [];
  bathroomAmenities: Array<string> = [];

  constructor(@Inject(MAT_DIALOG_DATA) private data: any) {
    this.room = this.data;
    let roomAmenities = this.data.roomAmenities.split(';');

    for (let roomAmenity of roomAmenities) {
      this.roomAmenities.push(roomAmenity.split(': ')[1]);
    }

    this.bathroomAmenities = this.data.bathroomAmenities.split(';');

    let beds = this.data.beds.split(';');

    for (let bed of beds) {
      this.beds.push(bed.split(': '));
    }
  }
}
