import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmDeleteReservationComponent } from './confirm-delete-reservation.component';

describe('ConfirmDeleteReservationComponent', () => {
  let component: ConfirmDeleteReservationComponent;
  let fixture: ComponentFixture<ConfirmDeleteReservationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ConfirmDeleteReservationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ConfirmDeleteReservationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
