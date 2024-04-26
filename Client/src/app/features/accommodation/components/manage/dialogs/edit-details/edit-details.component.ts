import { Component } from '@angular/core';
import { Constants } from '../../../../../../shared/interfaces/Constants';

@Component({
  selector: 'app-edit-details',
  templateUrl: './edit-details.component.html',
  styleUrl: './edit-details.component.css'
})
export class EditDetailsComponent {
  accommodationFacilitiesList: Array<any> = Constants.AccommodationFacilitiesList;
  timeOptions: Array<string> = Constants.TimeOptions;

}
