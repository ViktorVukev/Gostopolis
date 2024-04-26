import { Component } from '@angular/core';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrl: './reservations.component.css'
})
export class ReservationsComponent {

  displayedColumnsAccommodation: string[] = ['checkIn', 'checkOut', 'guests', 'name', 'dateOfBirth'];
  dataSourceAccommodation = [
    { checkIn: '112/02/2022', checkOut: '12/02/2022', guests: '50', name: 'Иван', dateOfBirth: '12/02/2022' }
  ];
  displayedColumnsRestaurant: string[] = ['date', 'time', 'guests', 'name'];
  dataSourceRestaurant = [
    { date: '112/02/2022', time: '12:00', guests: '50', name: 'Иван' }
  ];

}
