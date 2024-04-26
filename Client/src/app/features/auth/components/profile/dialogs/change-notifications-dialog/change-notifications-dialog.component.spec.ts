import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeNotificationsDialogComponent } from './change-notifications-dialog.component';

describe('ChangeNotificationsDialogComponent', () => {
  let component: ChangeNotificationsDialogComponent;
  let fixture: ComponentFixture<ChangeNotificationsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChangeNotificationsDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ChangeNotificationsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
