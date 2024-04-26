import {Component, OnInit} from '@angular/core';
import { SpinnerService } from './core/services/spinner.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'gostopolis-client';

  loading: number = 1;

  constructor(public spinnerService: SpinnerService) { }

  ngOnInit(): void {
    this.spinnerService.spinnerCounter$.subscribe(res => this.loading = res);
  }

}
