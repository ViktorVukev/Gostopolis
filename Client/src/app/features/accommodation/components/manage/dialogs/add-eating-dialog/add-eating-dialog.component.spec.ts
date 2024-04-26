import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEatingDialogComponent } from './add-eating-dialog.component';

describe('AddEatingDialogComponent', () => {
  let component: AddEatingDialogComponent;
  let fixture: ComponentFixture<AddEatingDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddEatingDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddEatingDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
