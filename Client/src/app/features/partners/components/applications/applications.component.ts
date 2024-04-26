import { Component, OnInit } from '@angular/core';
import { Application } from '../../../../shared/interfaces/Application';
import { PartnersService } from '../../services/partners.service';

@Component({
  selector: 'app-applications',
  templateUrl: './applications.component.html',
  styleUrl: './applications.component.css'
})
export class ApplicationsComponent implements OnInit {
  displayedColumns: string[] = ['id', 'createdOn', 'documentUrl', 'approvedOn', 'status'];
  dataSource!: Array<Application>;
  hasPendingApplication: boolean = false;
  isPartner: boolean = false;
  canApply: boolean = true;

  constructor(
    private partnersService: PartnersService) { }

  ngOnInit(): void {
    this.partnersService.applications().subscribe(res => {
      this.dataSource = res;
      this.canApply = this.dataSource.length <= 5;
    });

    this.partnersService
      .isPartner()
      .subscribe(res => {
        this.isPartner = res;
      });

    this.partnersService
      .hasPendingApplication()
      .subscribe(res => {
        this.hasPendingApplication = res;
      });
    }
}
