import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeProfileImageDialogComponent } from './change-profile-image-dialog.component';

describe('ChangeProfileImageDialogComponent', () => {
  let component: ChangeProfileImageDialogComponent;
  let fixture: ComponentFixture<ChangeProfileImageDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChangeProfileImageDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ChangeProfileImageDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
