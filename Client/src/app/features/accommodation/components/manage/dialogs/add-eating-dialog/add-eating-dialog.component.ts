import { Component } from '@angular/core';
import { Constants } from '../../../../../../shared/interfaces/Constants';

@Component({
  selector: 'app-add-eating-dialog',
  templateUrl: './add-eating-dialog.component.html',
  styleUrl: './add-eating-dialog.component.css'
})
export class AddEatingDialogComponent {

  timeOptions: Array<string> = Constants.TimeOptions;

}
